using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using PokeData.Constants;
using PokeData.Extensions;
using PokeData.Security;

namespace PokeData.Authentication;

internal class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
  private readonly IUserService _userService;

  public BasicAuthenticationHandler(IUserService userService, IOptionsMonitor<BasicAuthenticationOptions> options,
    ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
  {
    _userService = userService;
  }

  protected override Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    if (Context.Request.Headers.TryGetValue(Headers.Authorization, out StringValues authorization))
    {
      string? value = authorization.Single();
      if (!string.IsNullOrWhiteSpace(value))
      {
        string[] values = value.Split();
        if (values.Length != 2)
        {
          return Task.FromResult(AuthenticateResult.Fail($"The Authorization header value is not valid: '{value}'."));
        }
        else if (values[0] == Schemes.Basic)
        {
          byte[] bytes = Convert.FromBase64String(values[1]);
          string credentials = Encoding.UTF8.GetString(bytes);
          int index = credentials.IndexOf(':');
          if (index <= 0)
          {
            return Task.FromResult(AuthenticateResult.Fail($"The Basic credentials are not valid: '{credentials}'."));
          }

          try
          {
            string username = credentials[..index];
            string password = credentials[(index + 1)..];
            User? user = _userService.Authenticate(username, password);
            if (user == null)
            {
              return Task.FromResult(AuthenticateResult.Fail("The specified credentials did not match."));
            }

            Context.SetUser(user);

            ClaimsPrincipal principal = new(user.CreateClaimsIdentity(Scheme.Name));
            AuthenticationTicket ticket = new(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
          }
          catch (Exception exception)
          {
            return Task.FromResult(AuthenticateResult.Fail(exception));
          }
        }
      }
    }

    return Task.FromResult(AuthenticateResult.NoResult());
  }
}
