using PokeData.Application.Resources;
using PokeData.Domain.Resources;

namespace PokeData.Infrastructure.PokeApi;

internal record ExtractedResource<T>
{
  public string Json { get; }
  public T Value { get; }

  private ExtractedResource(string json, T value)
  {
    Json = json;
    Value = value;
  }

  public static ExtractedResource<T> FromJson(string json)
  {
    T value = JsonSerializer.Deserialize<T>(json)
      ?? throw new InvalidOperationException($"The resource could not be deserialized.{Environment.NewLine}Json: {json}");

    return new ExtractedResource<T>(json, value);
  }

  public Resource CreateResource(int identifier, string source)
    => CreateResource(identifier.ToString(), source);
  public Resource CreateResource(string identifier, string source)
    => new(ResourceId.Create<T>(identifier), new SourceUnit(source), new JsonStringUnit(Json));
}
