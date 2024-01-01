using Logitar.EventSourcing;
using PokeData.Contracts;
using PokeData.Contracts.Actors;
using PokeData.Contracts.Generations;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Roster;
using PokeData.Contracts.Types;
using PokeData.EntityFrameworkCore.Relational.Entities;

namespace PokeData.EntityFrameworkCore.Relational;

internal class Mapper
{
  private static readonly Actor _system = new();

  private readonly Dictionary<ActorId, Actor> _actors = [];

  public Mapper()
  {
  }
  public Mapper(IEnumerable<Actor> actors)
  {
    foreach (Actor actor in actors)
    {
      ActorId id = new(actor.Id);
      _actors[id] = actor;
    }
  }

  public virtual Actor ToActor(ActorEntity actor) => new()
  {
    Id = actor.Id,
    Type = actor.Type,
    IsDeleted = actor.IsDeleted,
    DisplayName = actor.DisplayName,
    EmailAddress = actor.EmailAddress,
    PictureUrl = actor.PictureUrl
  };

  public Generation ToGeneration(GenerationEntity source, Region? mainRegion)
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

    MapAggregate(source, destination);

    return destination;
  }

  public PokemonType ToPokemonType(PokemonTypeEntity source)
  {
    PokemonType destination = new()
    {
      Number = (byte)source.PokemonTypeId,
      UniqueName = source.UniqueName,
      DisplayName = source.DisplayName
    };

    MapAggregate(source, destination);

    return destination;
  }

  public Region ToRegion(RegionEntity source) => ToRegion(source, mainGeneration: null);
  public Region ToRegion(RegionEntity source, Generation? mainGeneration)
  {
    Region destination = new()
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

    MapAggregate(source, destination);

    return destination;
  }

  public PokemonRoster ToRoster(IEnumerable<PokemonSpeciesEntity> speciesList)
  {
    PokemonRoster roster = new();

    foreach (PokemonSpeciesEntity species in speciesList)
    {
      RosterItem? item = ToRosterItem(species);
      if (item != null)
      {
        roster.Items.Add(item);
      }
    }

    return roster;
  }
  public RosterItem? ToRosterItem(PokemonSpeciesEntity entity)
  {
    RosterInfo? source = ToRosterInfo(entity);
    if (source == null)
    {
      return null;
    }

    return new()
    {
      SpeciesId = entity.PokemonSpeciesId,
      Source = source,
      Destination = entity.Roster == null ? null : ToRosterInfo(entity.Roster)
    };
  }
  public virtual RosterInfo? ToRosterInfo(PokemonSpeciesEntity source)
  {
    RegionEntity? region = source.Generation?.MainRegion;
    PokemonVarietyEntity? variety = source.Varieties.SingleOrDefault(variety => variety.IsDefault);
    if (region == null || variety?.PrimaryType == null)
    {
      return null;
    }

    return new()
    {
      Number = (ushort)source.PokemonSpeciesId,
      Name = source.DisplayName ?? source.UniqueName,
      Category = source.Category,
      Region = region.DisplayName ?? region.UniqueName,
      PrimaryType = variety.PrimaryType.DisplayName ?? variety.PrimaryType.UniqueName,
      SecondaryType = variety.SecondaryType?.DisplayName ?? variety.SecondaryType?.UniqueName,
      IsBaby = source.IsBaby,
      IsLegendary = source.IsLegendary,
      IsMythical = source.IsMythical
    };
  }
  public virtual RosterInfo? ToRosterInfo(PokemonRosterEntity source)
  {
    if (source.Region == null || source.PrimaryType == null)
    {
      return null;
    }

    return new()
    {
      Number = (ushort)source.Number,
      Name = source.Name,
      Category = source.Category,
      Region = source.Region.DisplayName ?? source.Region.UniqueName,
      PrimaryType = source.PrimaryType.DisplayName ?? source.PrimaryType.UniqueName,
      SecondaryType = source.SecondaryType?.DisplayName ?? source.SecondaryType?.UniqueName,
      IsBaby = source.IsBaby,
      IsLegendary = source.IsLegendary,
      IsMythical = source.IsMythical
    };
  }

  protected void MapAggregate(AggregateEntity source, Aggregate destination)
  {
    destination.Version = source.Version;
    destination.CreatedBy = GetActor(source.CreatedBy);
    destination.CreatedOn = AsUniversalTime(source.CreatedOn);
    destination.UpdatedBy = GetActor(source.UpdatedBy);
    destination.UpdatedOn = AsUniversalTime(source.UpdatedOn);
  }

  protected Actor GetActor(string id) => GetActor(new ActorId(id));
  protected Actor GetActor(ActorId id) => _actors.TryGetValue(id, out Actor? actor) ? actor : _system;

  protected static DateTime AsUniversalTime(DateTime dateTime) => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
}
