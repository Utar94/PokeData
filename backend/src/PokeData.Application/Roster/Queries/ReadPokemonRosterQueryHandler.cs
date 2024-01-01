using MediatR;
using PokeData.Application.Regions;
using PokeData.Application.Types;
using PokeData.Contracts.Regions;
using PokeData.Contracts.Roster;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Application.Roster.Queries;

internal class ReadPokemonRosterQueryHandler : IRequestHandler<ReadPokemonRosterQuery, PokemonRoster>
{
  private readonly IRegionQuerier _regionQuerier;
  private readonly IPokemonRosterQuerier _rosterQuerier;
  private readonly IPokemonTypeQuerier _typeQuerier;

  public ReadPokemonRosterQueryHandler(IRegionQuerier regionQuerier, IPokemonRosterQuerier rosterQuerier, IPokemonTypeQuerier typeQuerier)
  {
    _regionQuerier = regionQuerier;
    _rosterQuerier = rosterQuerier;
    _typeQuerier = typeQuerier;
  }

  public async Task<PokemonRoster> Handle(ReadPokemonRosterQuery _, CancellationToken cancellationToken)
  {
    PokemonRoster roster = await _rosterQuerier.ReadAsync(cancellationToken);

    SearchResults<Region> regions = await _regionQuerier.SearchAsync(new SearchRegionsPayload(), cancellationToken);
    Dictionary<string, int> regionCount = new(capacity: regions.Items.Count);
    foreach (Region region in regions.Items)
    {
      string key = region.DisplayName ?? region.UniqueName;
      regionCount[key] = 0;
    }

    SearchResults<PokemonType> types = await _typeQuerier.SearchAsync(new SearchPokemonTypesPayload(), cancellationToken);
    Dictionary<string, int> typeCount = new(capacity: types.Items.Count);
    foreach (PokemonType type in types.Items)
    {
      string key = type.DisplayName ?? type.UniqueName;
      typeCount[key] = 0;
    }

    ushort selected = 0;
    ushort selectedTypes = 0;
    foreach (RosterItem item in roster.Items)
    {
      if (item.Destination != null)
      {
        selected++;
        selectedTypes++;

        regionCount[item.Destination.Region]++;
        typeCount[item.Destination.PrimaryType]++;

        if (item.Destination.SecondaryType != null)
        {
          selectedTypes++;
          typeCount[item.Destination.SecondaryType]++;
        }
      }
    }

    roster.Stats = new RosterStatistics
    {
      Count = selected,
      Regions = regionCount.Select(pair => RosterStatistic.Create(pair.Key, pair.Value, selected)).ToList(),
      Types = typeCount.Select(pair => RosterStatistic.Create(pair.Key, pair.Value, selectedTypes)).ToList()
    };

    return roster;
  }
}
