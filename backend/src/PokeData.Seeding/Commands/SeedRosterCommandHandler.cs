﻿using CsvHelper;
using MediatR;
using PokeData.Contracts.Roster;
using PokeData.Domain.Roster;
using PokeData.Domain.Species;
using PokeData.Seeding.Models;

namespace PokeData.Seeding.Commands;

internal class SeedRosterCommandHandler : INotificationHandler<SeedRosterCommand>
{
  private readonly ILogger<SeedRosterCommandHandler> _logger;
  private readonly IPokemonRosterRepository _pokemonRosterRepository;
  private readonly IPokemonRosterService _pokemonRosterService;
  private readonly IPokemonSpeciesRepository _pokemonSpeciesRepository;
  private readonly bool _resetRoster;

  public SeedRosterCommandHandler(IConfiguration configuration, ILogger<SeedRosterCommandHandler> logger,
    IPokemonRosterRepository pokemonRosterRepository, IPokemonRosterService pokemonRosterService, IPokemonSpeciesRepository pokemonSpeciesRepository)
  {
    _logger = logger;
    _pokemonRosterRepository = pokemonRosterRepository;
    _pokemonRosterService = pokemonRosterService;
    _pokemonSpeciesRepository = pokemonSpeciesRepository;
    _resetRoster = configuration.GetValue<bool>("ResetRoster");
  }

  public async Task Handle(SeedRosterCommand command, CancellationToken cancellationToken)
  {
    if (_resetRoster)
    {
      int deleted = await _pokemonRosterRepository.DeleteAllAsync(cancellationToken);
      _logger.LogInformation("Deleted {Count} Pokémon roster items.", deleted);
    }

    string[] paths = Directory.GetFiles(command.Path, "*.csv");
    _logger.LogInformation("Found {Count} roster files to seed.", paths.Length);

    foreach (string path in paths)
    {
      using StreamReader reader = new(path, command.Encoding);
      using CsvReader csv = new(reader, CultureInfo.InvariantCulture);

      RosterItemData[] records = csv.GetRecords<RosterItemData>().ToArray();
      _logger.LogInformation("Found {Count} roster items to seed.", records.Length);

      foreach (RosterItemData record in records)
      {
        PokemonSpecies? species = null;
        if (record.Number.HasValue)
        {
          species = await _pokemonSpeciesRepository.LoadAsync(record.Number.Value, cancellationToken);
        }
        else if (!string.IsNullOrWhiteSpace(record.Name))
        {
          species = await _pokemonSpeciesRepository.LoadAsync(record.Name.Trim(), cancellationToken);
        }

        if (species == null)
        {
          _logger.LogWarning("The Pokémon species for seed #{Id} could not be found.", record.SpeciesId);
          continue;
        }

        try
        {
          SaveRosterItemPayload payload = record.ToRosterItemPayload(species);
          await _pokemonRosterService.SaveItemAsync(record.SpeciesId, payload, cancellationToken);
          _logger.LogInformation("Seeded Pokémon #{Id}.", record.SpeciesId);
        }
        catch (Exception exception)
        {
          _logger.LogWarning(exception, "The Pokémon roster #{Id} could not be seeded.", record.SpeciesId);
        }
      }
    }
  }
}
