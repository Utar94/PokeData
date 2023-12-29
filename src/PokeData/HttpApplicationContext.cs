using Logitar.EventSourcing;
using PokeData.Application;
using PokeData.Extensions;
using PokeData.Security;

namespace PokeData;

internal class HttpApplicationContext : IApplicationContext
{
  private static readonly ActorId _system = new();

  private readonly IHttpContextAccessor _httpContextAccessor;

  public HttpApplicationContext(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  protected HttpContext Context => _httpContextAccessor.HttpContext
    ?? throw new InvalidOperationException($"The {nameof(_httpContextAccessor.HttpContext)} is required.");

  public ActorId ActorId
  {
    get
    {
      User? user = Context.GetUser();
      if (user != null)
      {
        return new(user.Id);
      }

      return _system;
    }
  }
}
