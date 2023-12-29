using Logitar.Security.Claims;
using PokeData.Security;

namespace PokeData.Extensions;

internal static class ClaimExtensions
{
  public static ClaimsIdentity CreateClaimsIdentity(this User user, string? authenticationType = null)
  {
    ClaimsIdentity identity = new(authenticationType);

    identity.AddClaim(new(Rfc7519ClaimNames.Subject, user.Id));
    identity.AddClaim(new(Rfc7519ClaimNames.Username, user.Username));
    identity.AddClaim(ClaimHelper.Create(Rfc7519ClaimNames.AuthenticationTime, DateTime.UtcNow));

    if (user.EmailAddress != null)
    {
      identity.AddClaim(new(Rfc7519ClaimNames.EmailAddress, user.EmailAddress));
    }

    if (user.FullName != null)
    {
      identity.AddClaim(new(Rfc7519ClaimNames.FullName, user.FullName));
    }

    if (user.PictureUrl != null)
    {
      identity.AddClaim(new(Rfc7519ClaimNames.Picture, user.PictureUrl));
    }

    return identity;
  }
}
