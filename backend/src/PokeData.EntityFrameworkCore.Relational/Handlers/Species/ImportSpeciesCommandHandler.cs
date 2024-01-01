using MediatR;
using Microsoft.EntityFrameworkCore;
using PokeData.Application;
using PokeData.Application.Species.Commands;
using PokeData.Domain.Species;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational.Handlers.Species;

internal class ImportSpeciesCommandHandler : INotificationHandler<ImportSpeciesCommand>
{
  private readonly PokemonContext _context;
  private readonly IResourceService _resourceService;

  public ImportSpeciesCommandHandler(PokemonContext context, IResourceService resourceService)
  {
    _context = context;
    _resourceService = resourceService;
  }

  public async Task Handle(ImportSpeciesCommand command, CancellationToken cancellationToken)
  {
    IEnumerable<PokemonSpecies> species = await ExtractAsync(command.Ids, cancellationToken);
    await LoadAsync(species, cancellationToken);
  }

  private async Task<IEnumerable<PokemonSpecies>> ExtractAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
  {
    List<PokemonSpecies> speciesList = new(capacity: ids.Count());

    foreach (string id in ids)
    {
      PokemonSpecies? species = await _resourceService.GetSpeciesAsync(id, cancellationToken)
        ?? throw new InvalidOperationException($"The Pokémon species 'Id={id}' could not be found.");
      speciesList.Add(species);
    }

    return speciesList.AsReadOnly();
  }

  private async Task LoadAsync(IEnumerable<PokemonSpecies> speciesList, CancellationToken cancellationToken)
  {
    Dictionary<int, GenerationEntity> generations = await _context.Generations
      .ToDictionaryAsync(x => x.GenerationId, x => x, cancellationToken);
    Dictionary<int, RegionEntity> regions = await _context.Regions
      .ToDictionaryAsync(x => x.RegionId, x => x, cancellationToken);
    Dictionary<int, PokemonSpeciesEntity> speciesEntities = await _context.PokemonSpecies
      .ToDictionaryAsync(x => x.PokemonSpeciesId, x => x, cancellationToken);
    Dictionary<int, PokemonTypeEntity> types = await _context.PokemonTypes
      .ToDictionaryAsync(x => x.PokemonTypeId, x => x, cancellationToken);
    Dictionary<int, PokemonVarietyEntity> varieties = await _context.PokemonVarieties
      .ToDictionaryAsync(x => x.PokemonVarietyId, x => x, cancellationToken);

    foreach (PokemonSpecies species in speciesList)
    {
      if (species.Generation != null)
      {
        if (generations.TryGetValue(species.Generation.Number, out GenerationEntity? generation))
        {
          generation.Update(species.Generation);
        }
        else if (species.Generation.MainRegion != null)
        {
          if (regions.TryGetValue(species.Generation.MainRegion.Number, out RegionEntity? region))
          {
            region.Update(species.Generation.MainRegion);
          }
          else
          {
            region = new(species.Generation.MainRegion);
            regions[region.RegionId] = region;

            _context.Regions.Add(region);
          }

          generation = new(species.Generation, region);
          generations[generation.GenerationId] = generation;

          _context.Generations.Add(generation);
        }
        else
        {
          continue;
        }

        if (speciesEntities.TryGetValue(species.Number, out PokemonSpeciesEntity? speciesEntity))
        {
          speciesEntity.Update(species, generation);
        }
        else
        {
          speciesEntity = new(species, generation);
          speciesEntities[speciesEntity.PokemonSpeciesId] = speciesEntity;

          _context.PokemonSpecies.Add(speciesEntity);
        }

        foreach (PokemonVariety variety in species.Varieties)
        {
          if (variety.PrimaryType != null)
          {
            if (types.TryGetValue(variety.PrimaryType.Number, out PokemonTypeEntity? primaryType))
            {
              primaryType.Update(variety.PrimaryType);
            }
            else
            {
              primaryType = new(variety.PrimaryType);
              types[primaryType.PokemonTypeId] = primaryType;

              _context.PokemonTypes.Add(primaryType);
            }

            PokemonTypeEntity? secondaryType = null;
            if (variety.SecondaryType != null)
            {
              if (types.TryGetValue(variety.SecondaryType.Number, out secondaryType))
              {
                secondaryType.Update(variety.SecondaryType);
              }
              else
              {
                secondaryType = new(variety.SecondaryType);
                types[secondaryType.PokemonTypeId] = secondaryType;

                _context.PokemonTypes.Add(secondaryType);
              }
            }

            if (varieties.TryGetValue(variety.Number, out PokemonVarietyEntity? varietyEntity))
            {
              varietyEntity.Update(variety, primaryType, secondaryType);
            }
            else
            {
              varietyEntity = new(variety, speciesEntity, primaryType, secondaryType);
              varieties[varietyEntity.PokemonVarietyId] = varietyEntity;

              speciesEntity.Varieties.Add(varietyEntity);
            }
          }
        }

        await _context.SaveChangesAsync(cancellationToken);
      }
    }
  }
}
