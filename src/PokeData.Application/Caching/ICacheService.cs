namespace PokeData.Application.Caching;

public interface ICacheService
{
  string? GetResourceJson(string source);
  void SetResourceJson(string source, string? json);
}
