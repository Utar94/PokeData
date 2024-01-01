using MediatR;
using PokeData.Application.Types.Queries;
using PokeData.Contracts.Search;
using PokeData.Contracts.Types;

namespace PokeData.Application.Types;

internal class PokemonTypeService : IPokemonTypeService
{
  private readonly IMediator _mediator;

  public PokemonTypeService(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<PokemonType?> ReadAsync(byte? number, string? uniqueName, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new ReadPokemonTypeQuery(number, uniqueName), cancellationToken);
  }

  public async Task<SearchResults<PokemonType>> SearchAsync(SearchPokemonTypesPayload payload, CancellationToken cancellationToken)
  {
    return await _mediator.Send(new SearchPokemonTypesQuery(payload), cancellationToken);
  }
}
