using PokeData.Domain.Resources;

namespace PokeData.Application.Caching;

public interface ICacheService
{
  Resource? GetResource(Uri source);
  void SetResource(Resource resource);
}
