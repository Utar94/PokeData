using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record PokemonSpeciesVariety
{
  [JsonPropertyName("is_default")]
  public bool IsDefault { get; set; }

  [JsonPropertyName("pokemon")]
  public NamedAPIResource? Pokemon { get; set; }
}
