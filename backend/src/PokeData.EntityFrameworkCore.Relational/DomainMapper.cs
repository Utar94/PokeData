using PokeData.Domain;
using PokeData.Domain.Generations;
using PokeData.Domain.Regions;
using PokeData.Domain.Resources;
using PokeData.Domain.Species;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

internal static class DomainMapper
{
  public static Generation ToGeneration(GenerationEntity source) => ToGeneration(source, mainRegion: null);
  public static Generation ToGeneration(GenerationEntity source, RegionAggregate? mainRegion)
  {
    Generation destination = new()
    {
      Number = (byte)source.GenerationId,
      UniqueName = source.UniqueName,
      DisplayName = source.DisplayName,
      MainRegion = mainRegion
    };

    if (destination.MainRegion == null && source.MainRegion != null)
    {
      destination.MainRegion = ToRegion(source.MainRegion, destination);
    }

    return destination;
  }

  public static RegionAggregate ToRegion(RegionEntity source) => ToRegion(source, mainGeneration: null);
  public static RegionAggregate ToRegion(RegionEntity source, Generation? mainGeneration)
  {
    RegionAggregate destination = new()
    {
      Number = (byte)source.RegionId,
      UniqueName = source.UniqueName,
      DisplayName = source.DisplayName,
      MainGeneration = mainGeneration
    };

    if (destination.MainGeneration == null && source.MainGeneration != null)
    {
      destination.MainGeneration = ToGeneration(source.MainGeneration, destination);
    }

    return destination;
  }

  public static Resource ToResource(ResourceEntity resource)
    => new(new Uri(resource.Source), new Content(resource.ContentType, resource.ContentText));

  public static PokemonSpecies ToPokemonSpecies(PokemonSpeciesEntity source)
  {
    PokemonSpecies destination = new()
    {
      Number = (ushort)source.PokemonSpeciesId,
      Order = (ushort)source.Order,
      IsBaby = source.IsBaby,
      IsLegendary = source.IsLegendary,
      IsMythical = source.IsMythical,
      HasGenderDifferences = source.HasGenderDifferences,
      CanSwitchForm = source.CanSwitchForm,
      UniqueName = source.UniqueName,
      DisplayName = source.DisplayName,
      Category = source.Category,
      GenderRatio = source.GenderRatio,
      CatchRate = source.CatchRate,
      HatchTime = source.HatchTime,
      BaseFriendship = source.BaseFriendship,
      Generation = source.Generation == null ? null : ToGeneration(source.Generation)
    };

    foreach (PokemonVarietyEntity entity in source.Varieties)
    {
      PokemonVariety? variety = ToPokemonVariety(entity, destination);
      if (variety != null)
      {
        destination.Varieties.Add(variety);
      }
    }

    return destination;
  }
  private static PokemonVariety? ToPokemonVariety(PokemonVarietyEntity source, PokemonSpecies? species)
  {
    if (source.PrimaryType == null)
    {
      return null;
    }

    return new()
    {
      Number = (ushort)source.PokemonVarietyId,
      Order = (ushort)source.Order,
      UniqueName = source.UniqueName,
      PrimaryType = ToPokemonType(source.PrimaryType),
      SecondaryType = source.SecondaryType == null ? null : ToPokemonType(source.SecondaryType),
      Height = source.Height,
      Weight = source.Weight,
      BaseExperienceYield = source.BaseExperienceYield,
      Species = species,
      IsDefault = source.IsDefault
    };
  }

  public static PokemonType ToPokemonType(PokemonTypeEntity source) => new()
  {
    Number = (byte)source.PokemonTypeId,
    UniqueName = source.UniqueName,
    DisplayName = source.DisplayName
  };
}
