namespace PokeData.Security;

internal interface IUserService
{
  User? Authenticate(string username, string password);
}
