using MediatR;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types.Queries;

internal class ReadPokemonTypeQueryHandler : IRequestHandler<ReadPokemonTypeQuery, PokemonType?>
{
  private readonly IPokemonTypeQuerier _typeQuerier;

  public ReadPokemonTypeQueryHandler(IPokemonTypeQuerier typeQuerier)
  {
    _typeQuerier = typeQuerier;
  }

  public async Task<PokemonType?> Handle(ReadPokemonTypeQuery query, CancellationToken cancellationToken)
  {
    Dictionary<ushort, PokemonType> results = new(capacity: 2);

    if (query.Number.HasValue)
    {
      PokemonType? type = await _typeQuerier.ReadAsync(query.Number.Value, cancellationToken);
      if (type != null)
      {
        results[type.Number] = type;
      }
    }
    if (!string.IsNullOrWhiteSpace(query.UniqueName))
    {
      PokemonType? type = await _typeQuerier.ReadAsync(query.UniqueName, cancellationToken);
      if (type != null)
      {
        results[type.Number] = type;
      }
    }

    if (results.Count > 1)
    {
      throw new TooManyResultsException<PokemonType>(expectedCount: 1, actualCount: results.Count);
    }

    return results.Values.SingleOrDefault();
  }
}
