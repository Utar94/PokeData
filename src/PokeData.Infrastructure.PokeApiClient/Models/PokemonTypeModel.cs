using System.Text.Json.Serialization;

namespace PokeData.Infrastructure.PokeApiClient.Models;

internal record PokemonTypeModel
{
  [JsonPropertyName("slot")]
  public int Slot { get; set; }

  [JsonPropertyName("type")]
  public NamedAPIResource? Type { get; set; }
}
