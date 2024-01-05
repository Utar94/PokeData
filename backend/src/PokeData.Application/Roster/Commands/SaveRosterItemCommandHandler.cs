using FluentValidation;
using Logitar;
using MediatR;
using PokeData.Application.Roster.Validators;
using PokeData.Domain;
using PokeData.Domain.Regions;
using PokeData.Domain.Roster;
using PokeData.Domain.Species;
using PokeData.Domain.Types;

namespace PokeData.Application.Roster.Commands;

internal class SaveRosterItemCommandHandler : IRequestHandler<SaveRosterItemCommand, Unit>
{
  private readonly IRegionRepository _regionRepository;
  private readonly IPokemonRosterRepository _rosterRepository;
  private readonly IPokemonSpeciesRepository _speciesRepository;
  private readonly IPokemonTypeRepository _typeRepository;

  public SaveRosterItemCommandHandler(IRegionRepository regionRepository, IPokemonRosterRepository rosterRepository,
    IPokemonSpeciesRepository speciesRepository, IPokemonTypeRepository typeRepository)
  {
    _regionRepository = regionRepository;
    _rosterRepository = rosterRepository;
    _speciesRepository = speciesRepository;
    _typeRepository = typeRepository;
  }

  public async Task<Unit> Handle(SaveRosterItemCommand command, CancellationToken cancellationToken)
  {
    Contracts.Roster.SaveRosterItemPayload payload = command.Payload;
    new SaveRosterItemPayloadValidator().ValidateAndThrow(payload);

    PokemonSpecies species = await _speciesRepository.LoadAsync(command.SpeciesId, cancellationToken)
      ?? throw new AggregateNotFoundException<PokemonSpecies>(command.SpeciesId.ToString(), nameof(command.SpeciesId));

    RegionAggregate region = await _regionRepository.LoadAsync(payload.Region, cancellationToken)
      ?? throw new AggregateNotFoundException<RegionAggregate>(payload.Region.ToString(), nameof(payload.Region));

    PokemonType primaryType = await _typeRepository.LoadAsync(payload.PrimaryType, cancellationToken)
      ?? throw new AggregateNotFoundException<PokemonType>(payload.PrimaryType.ToString(), nameof(payload.PrimaryType));
    PokemonType? secondaryType = null;
    if (!string.IsNullOrWhiteSpace(payload.SecondaryType))
    {
      secondaryType = await _typeRepository.LoadAsync(payload.SecondaryType, cancellationToken)
        ?? throw new AggregateNotFoundException<PokemonType>(payload.SecondaryType, nameof(payload.SecondaryType));
    }

    PokemonRoster roster = new()
    {
      Species = species,
      Number = payload.Number,
      Name = payload.Name.Trim(),
      Category = payload.Category?.CleanTrim(),
      Region = region,
      PrimaryType = primaryType,
      SecondaryType = secondaryType,
      IsBaby = payload.IsBaby,
      IsLegendary = payload.IsLegendary,
      IsMythical = payload.IsMythical
    };
    await _rosterRepository.SaveAsync(roster, cancellationToken);

    return Unit.Value;
  }
}
