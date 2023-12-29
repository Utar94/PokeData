using PokeData.Application.Caching;
using PokeData.Application.Resources;
using PokeData.Infrastructure.Models;

namespace PokeData.Infrastructure.PokeApi;

internal class ResourceExtractor : IResourceExtractor
{
  private readonly ICacheService _cache;
  private readonly HttpClient _client;
  private readonly IResourceQuerier _querier;
  private readonly Random _random = new();
  private readonly ResourceExtractorSettings _settings;

  public ResourceExtractor(ICacheService cache, HttpClient client, IResourceQuerier querier, ResourceExtractorSettings settings)
  {
    _cache = cache;
    _client = client;
    _querier = querier;
    _settings = settings;
  }

  public async Task<IEnumerable<Resource>> GetSpeciesAsync(string id, CancellationToken cancellationToken)
  {
    List<Resource> resources = [];

    string speciesUrl = $"https://pokeapi.co/api/v2/pokemon-species/{id}/";
    ExtractedResource<PokemonSpecies> species = await ExtractAsync<PokemonSpecies>(speciesUrl, cancellationToken);
    resources.Add(species.CreateResource(species.Value.Id, speciesUrl));

    if (species.Value.Generation != null)
    {
      ExtractedResource<Generation> generation = await ExtractAsync<Generation>(species.Value.Generation, cancellationToken);
      resources.Add(generation.CreateResource(generation.Value.Id, species.Value.Generation.Url));

      if (generation.Value.MainRegion != null)
      {
        ExtractedResource<Region> region = await ExtractAsync<Region>(generation.Value.MainRegion, cancellationToken);
        resources.Add(region.CreateResource(region.Value.Id, generation.Value.MainRegion.Url));
      }
    }

    foreach (PokemonSpeciesVariety variety in species.Value.Varieties)
    {
      if (variety.Pokemon != null)
      {
        ExtractedResource<Pokemon> pokemon = await ExtractAsync<Pokemon>(variety.Pokemon, cancellationToken);
        resources.Add(pokemon.CreateResource(pokemon.Value.Id, variety.Pokemon.Url));

        foreach (PokemonType type in pokemon.Value.Types)
        {
          if (type.Type != null)
          {
            ExtractedResource<Models.Type> typeResource = await ExtractAsync<Models.Type>(type.Type, cancellationToken);
            resources.Add(typeResource.CreateResource(typeResource.Value.Id, type.Type.Url));
          }
        }
      }
    }

    return resources;
  }

  private async Task<ExtractedResource<T>> ExtractAsync<T>(APIResource resource, CancellationToken cancellationToken)
    => await ExtractAsync<T>(resource.Url, cancellationToken);
  private async Task<ExtractedResource<T>> ExtractAsync<T>(string uriString, CancellationToken cancellationToken)
  {
    string? json = _cache.GetResourceJson(uriString);
    if (json == null)
    {
      Resource? resource = await _querier.ReadBySourceAsync(uriString, cancellationToken);
      json = resource?.Json.Value;
    }

    if (json != null)
    {
      try
      {
        ExtractedResource<T> result = ExtractedResource<T>.FromJson(json);
        _cache.SetResourceJson(uriString, json);
        return result;
      }
      catch (Exception)
      {
      }
    }

    await DelayAsync(cancellationToken);

    Uri requestUri = new(uriString);
    using HttpRequestMessage request = new(HttpMethod.Get, requestUri);
    using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
    try
    {
      response.EnsureSuccessStatusCode();
    }
    catch (Exception innerException)
    {
      HttpResponseDetail detail = await response.DetailAsync(cancellationToken);
      throw new HttpFailureException(detail, innerException);
    }

    json = await response.Content.ReadAsStringAsync(cancellationToken);
    try
    {
      ExtractedResource<T> result = ExtractedResource<T>.FromJson(json);
      _cache.SetResourceJson(uriString, json);
      return result;
    }
    catch (Exception innerException)
    {
      HttpResponseDetail detail = await response.DetailAsync(cancellationToken);
      throw new ResourceDeserializationException(detail, innerException);
    }
  }

  private async Task DelayAsync(CancellationToken cancellationToken)
  {
    if (_settings.Delay != null && _settings.Delay.Minimum >= 0 && _settings.Delay.Maximum > _settings.Delay.Minimum)
    {
      int millisecondsDelay = _random.Next(_settings.Delay.Minimum, _settings.Delay.Maximum + 1);
      await Task.Delay(millisecondsDelay, cancellationToken);
    }
  }
}
