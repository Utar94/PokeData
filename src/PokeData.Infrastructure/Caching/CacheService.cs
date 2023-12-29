using Microsoft.Extensions.Caching.Memory;
using PokeData.Application.Caching;

namespace PokeData.Infrastructure.Caching;

internal class CacheService : ICacheService
{
  private readonly IMemoryCache _memoryCache;

  public CacheService(IMemoryCache memoryCache)
  {
    _memoryCache = memoryCache;
  }

  public string? GetResourceJson(string source) => GetItem<string>(GetResourceJsonKey(source));
  public void SetResourceJson(string source, string? json)
  {
    string key = GetResourceJsonKey(source);
    if (json == null)
    {
      RemoveItem(key);
    }
    else
    {
      SetItem(key, json, TimeSpan.FromHours(1));
    }
  }
  private static string GetResourceJsonKey(string source) => $"ResourceJson:Url|{source}";

  private T? GetItem<T>(object key) => _memoryCache.TryGetValue(key, out object? value) ? (T?)value : default;
  private void RemoveItem(object key) => _memoryCache.Remove(key);
  private void SetItem<T>(object key, T value, TimeSpan? expiration = null)
  {
    if (expiration.HasValue)
    {
      _memoryCache.Set(key, value, expiration.Value);
    }
    else
    {
      _memoryCache.Set(key, value);
    }
  }
}
