using PokeData.Application;
using PokeData.Application.Caching;
using PokeData.Domain;
using PokeData.Domain.Generations;
using PokeData.Domain.Regions;
using PokeData.Domain.Resources;
using PokeData.Domain.Species;
using PokeData.Infrastructure.PokeApiClient.Models;

namespace PokeData.Infrastructure.PokeApiClient;

internal class ResourceService : IResourceService
{
  private const string LanguageName = "en";

  private readonly Random _random = new();

  private readonly ICacheService _cache;
  private readonly HttpClient _client;
  private readonly IResourceRepository _repository;

  public ResourceService(ICacheService cache, HttpClient client, IResourceRepository repository)
  {
    _cache = cache;
    _client = client;
    _repository = repository;
  }

  public async Task<PokemonSpecies?> GetSpeciesAsync(string id, CancellationToken cancellationToken)
  {
    Uri source = new($"https://pokeapi.co/api/v2/pokemon-species/{id}/");
    PokemonSpeciesModel? speciesModel = await ExtractAsync<PokemonSpeciesModel>(source, cancellationToken);
    if (speciesModel == null)
    {
      return null;
    }

    Generation? generation = null;
    if (speciesModel.Generation != null)
    {
      GenerationModel? generationModel = await ExtractAsync<GenerationModel>(speciesModel.Generation, cancellationToken);
      if (generationModel != null)
      {
        generation = new()
        {
          Number = (byte)generationModel.Id,
          UniqueName = generationModel.Name,
          DisplayName = generationModel.Names.SingleOrDefault(name => name.Language?.Name == LanguageName)?.Value
        };

        if (generationModel.MainRegion != null)
        {
          RegionModel? regionModel = await ExtractAsync<RegionModel>(generationModel.MainRegion, cancellationToken);
          if (regionModel != null)
          {
            RegionAggregate region = new()
            {
              Number = (byte)regionModel.Id,
              UniqueName = regionModel.Name,
              DisplayName = regionModel.Names.SingleOrDefault(name => name.Language?.Name == LanguageName)?.Value,
              MainGeneration = generation
            };
            generation.MainRegion = region;
          }
        }
      }
    }

    PokemonSpecies species = new()
    {
      Number = (ushort)speciesModel.Id,
      Order = (ushort)speciesModel.Order,
      IsBaby = speciesModel.IsBaby,
      IsLegendary = speciesModel.IsLegendary,
      IsMythical = speciesModel.IsMythical,
      HasGenderDifferences = speciesModel.HasGenderDifferences,
      CanSwitchForm = speciesModel.FormsSwitchable,
      UniqueName = speciesModel.Name,
      DisplayName = speciesModel.Names.SingleOrDefault(name => name.Language?.Name == LanguageName)?.Value,
      Category = speciesModel.Genera.SingleOrDefault(genus => genus.Language?.Name == LanguageName)?.Value,
      GenderRatio = speciesModel.GenderRate == -1 ? null : (1 - speciesModel.GenderRate / 8.0),
      CatchRate = (byte?)speciesModel.CaptureRate ?? 0,
      HatchTime = (byte?)speciesModel.HatchCounter ?? 0,
      BaseFriendship = (byte?)speciesModel.BaseHappiness ?? 0,
      Generation = generation,
      Varieties = new List<PokemonVariety>(capacity: speciesModel.Varieties.Count)
    };

    foreach (PokemonSpeciesVariety speciesVariety in speciesModel.Varieties)
    {
      if (speciesVariety.Pokemon != null)
      {
        Pokemon? pokemon = await ExtractAsync<Pokemon>(speciesVariety.Pokemon, cancellationToken);
        if (pokemon != null)
        {
          PokemonVariety variety = new()
          {
            Number = (ushort)pokemon.Id,
            Order = (ushort)pokemon.Order,
            UniqueName = pokemon.Name,
            Height = pokemon.Height / 10.0,
            Weight = pokemon.Weight / 10.0,
            BaseExperienceYield = (ushort?)pokemon.BaseExperience ?? 0,
            Species = species,
            IsDefault = speciesVariety.IsDefault
          };
          species.Varieties.Add(variety);

          foreach (PokemonTypeModel item in pokemon.Types)
          {
            if (item.Type != null)
            {
              TypeModel? typeModel = await ExtractAsync<TypeModel>(item.Type, cancellationToken);
              if (typeModel != null)
              {
                PokemonType type = new()
                {
                  Number = (byte)typeModel.Id,
                  UniqueName = typeModel.Name,
                  DisplayName = typeModel.Names.SingleOrDefault(name => name.Language?.Name == LanguageName)?.Value
                };

                switch (item.Slot)
                {
                  case 1:
                    variety.PrimaryType = type;
                    break;
                  case 2:
                    variety.SecondaryType = type;
                    break;
                  default:
                    throw new NotSupportedException($"The Pokémon type slot '{item.Slot}' is not supported.");
                }
              }
            }
          }
        }
      }
    }

    return species;
  }

  private async Task<T?> ExtractAsync<T>(APIResource resource, CancellationToken cancellationToken)
    => await ExtractAsync<T>(new Uri(resource.Url), cancellationToken);
  private async Task<T?> ExtractAsync<T>(Uri source, CancellationToken cancellationToken)
  {
    Resource? resource = _cache.GetResource(source) ?? await _repository.GetAsync(source, cancellationToken);
    if (resource != null)
    {
      _cache.SetResource(resource);
      return Deserialize<T>(resource.Content);
    }

    int millisecondsDelay = _random.Next(50, 500 + 1);
    await Task.Delay(millisecondsDelay, cancellationToken);

    using HttpRequestMessage request = new(HttpMethod.Get, source);
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

    string type = response.Content.Headers.ContentType?.MediaType ?? MediaTypeNames.Application.Json;
    string text = await response.Content.ReadAsStringAsync(cancellationToken);

    Content content = new(type, text);
    resource = new(source, content);

    await _repository.SaveAsync(resource, cancellationToken);

    _cache.SetResource(resource);
    return Deserialize<T>(content);
  }
  private static T? Deserialize<T>(Content content)
  {
    return content.Type switch
    {
      MediaTypeNames.Application.Json => JsonSerializer.Deserialize<T>(content.Text),
      _ => throw new NotSupportedException($"The content type '{content.Type}' is not supported."),
    };
  }
}
