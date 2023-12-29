using CsvHelper;
using PokeData.ETL.Entities;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace PokeData.ETL;

public class Worker : BackgroundService
{
  private const string ResourcesJsonPath = "resources.json";
  private const string SpeciesCsvPath = "species.csv";
  private const string SpeciesJsonPath = "species.json";

  private static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("en");
  private static readonly Encoding Encoding = Encoding.UTF8;

  private readonly HttpClient _client;
  private readonly ILogger<Worker> _logger;

  private readonly Random _random = new();
  private Dictionary<string, string> _resources = [];
  private Dictionary<string, PokemonSpecies> _species = [];

  public Worker(HttpClient client, ILogger<Worker> logger)
  {
    _client = client;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken cancellationToken)
  {
    Stopwatch chrono = Stopwatch.StartNew();

    await LoadAsync(cancellationToken);

    try
    {
      for (int id = 1; id <= 1025; id++)
      {
        string speciesUrl = $"https://pokeapi.co/api/v2/pokemon-species/{id}/";
        Models.PokemonSpecies? speciesModel = await GetResourceAsync<Models.PokemonSpecies>(speciesUrl, cancellationToken);
        if (speciesModel == null)
        {
          _logger.LogWarning("No species found at '{url}'.", speciesUrl);
        }
        else
        {
          PokemonSpecies species = PokemonSpecies.FromModel(speciesModel, Culture.Name);
          string key = $"urn:pokemon:species:id:{species.Number}";
          _species[key] = species;

          if (speciesModel.Generation != null)
          {
            Models.Generation? generationModel = await GetResourceAsync<Models.Generation>(speciesModel.Generation, cancellationToken);
            if (generationModel != null)
            {
              Generation generation = Generation.FromModel(generationModel, Culture.Name);
              species.Generation = generation;
              if (generationModel.MainRegion != null)
              {
                Models.Region? regionModel = await GetResourceAsync<Models.Region>(generationModel.MainRegion, cancellationToken);
                if (regionModel != null)
                {
                  generation.Region = Region.FromModel(regionModel, Culture.Name);
                }
              }
            }
          }

          foreach (Models.PokemonSpeciesVariety speciesVariety in speciesModel.Varieties)
          {
            if (speciesVariety.Pokemon != null)
            {
              Models.Pokemon? varietyModel = await GetResourceAsync<Models.Pokemon>(speciesVariety.Pokemon, cancellationToken);
              if (varietyModel != null)
              {
                PokemonVariety variety = PokemonVariety.FromModel(varietyModel);
                species.Varieties.Add(variety);

                IOrderedEnumerable<Models.PokemonType> pokemonTypes = varietyModel.Types.OrderBy(type => type.Slot);
                foreach (Models.PokemonType pokemonType in pokemonTypes)
                {
                  if (pokemonType.Type != null)
                  {
                    Models.Type? typeModel = await GetResourceAsync<Models.Type>(pokemonType.Type, cancellationToken);
                    if (typeModel != null)
                    {
                      PokemonType type = PokemonType.FromModel(typeModel, Culture.Name);
                      variety.Types.Add(type);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, "An unhandled exception has occurred.");
    }

    await SaveAsync(cancellationToken);

    chrono.Stop();
    _logger.LogInformation("Operation completed in {elapsed}ms.", chrono.ElapsedMilliseconds);
  }

  private async Task<T?> GetResourceAsync<T>(Models.APIResource resource, CancellationToken cancellationToken)
    => await GetResourceAsync<T>(resource.Url, cancellationToken);
  private async Task<T?> GetResourceAsync<T>(string url, CancellationToken cancellationToken)
  {
    if (!_resources.TryGetValue(url, out string? json))
    {
      int millisecondsDelay = _random.Next(1, 10 + 1) * 100;
      await Task.Delay(millisecondsDelay, cancellationToken);

      Uri requestUri = new(url);
      using HttpRequestMessage request = new(HttpMethod.Get, requestUri);
      using HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
      response.EnsureSuccessStatusCode();

      json = await response.Content.ReadAsStringAsync(cancellationToken);
      _resources[url] = json;
    }

    return JsonSerializer.Deserialize<T>(json);
  }

  private async Task LoadAsync(CancellationToken cancellationToken)
  {
    try
    {
      string resources = await File.ReadAllTextAsync(ResourcesJsonPath, Encoding, cancellationToken);
      _resources = JsonSerializer.Deserialize<Dictionary<string, string>>(resources) ?? [];
    }
    catch (Exception)
    {
    }

    try
    {
      string species = await File.ReadAllTextAsync(SpeciesJsonPath, Encoding, cancellationToken);
      _species = JsonSerializer.Deserialize<Dictionary<string, PokemonSpecies>>(species) ?? [];
    }
    catch (Exception)
    {
    }
  }
  private async Task SaveAsync(CancellationToken cancellationToken)
  {
    string resources = JsonSerializer.Serialize(_resources);
    await File.WriteAllTextAsync(ResourcesJsonPath, resources, Encoding, cancellationToken);

    string species = JsonSerializer.Serialize(_species);
    await File.WriteAllTextAsync(SpeciesJsonPath, species, Encoding, cancellationToken);

    IEnumerable<SpeciesLine> records = _species.Values.Select(species =>
    {
      Region? region = species.Generation?.Region;
      PokemonVariety? variety = species.Varieties.SingleOrDefault(variety => variety.IsDefault);

      SpeciesLine line = new()
      {
        Number = species.Number.ToString("0000"),
        Name = species.DisplayName ?? species.UniqueName
      };

      if (region != null)
      {
        line.Region = region.DisplayName ?? region.UniqueName;
      }

      if (variety != null)
      {
        PokemonType primaryType = variety.Types.First();
        line.PrimaryType = primaryType.DisplayName ?? primaryType.UniqueName;

        if (variety.Types.Count > 1)
        {
          PokemonType secondaryType = variety.Types.Skip(1).Single();
          line.SecondaryType = secondaryType.DisplayName ?? secondaryType.UniqueName;
        }
      }

      return line;
    });
    using StreamWriter writer = new(SpeciesCsvPath);
    using CsvWriter csv = new(writer, Culture);
    csv.WriteRecords(records);
  }
}
