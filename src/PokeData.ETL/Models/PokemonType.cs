using System.Text.Json.Serialization;

namespace PokeData.ETL.Models;

internal record PokemonType
{
  [JsonPropertyName("slot")]
  public int Slot { get; set; }

  [JsonPropertyName("type")]
  public NamedAPIResource? Type { get; set; }
}
