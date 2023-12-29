namespace PokeData.Security;

internal class UserService : IUserService
{
  private readonly Dictionary<string, User> _usersById;
  private readonly Dictionary<string, User> _usersByUsername;

  public UserService(IConfiguration configuration)
  {
    User[] users = configuration.GetSection("Users").Get<User[]>() ?? [];
    _usersById = new(capacity: users.Length);
    _usersByUsername = new(capacity: users.Length);
    foreach (User user in users)
    {
      _usersById[user.Id] = user;
      _usersByUsername[Normalize(user.Username)] = user;
    }
  }

  public User? Authenticate(string username, string password)
  {
    string usernameNormalized = Normalize(username);
    if (_usersByUsername.TryGetValue(usernameNormalized, out User? user))
    {
      Pbkdf2 pbkdf2 = Pbkdf2.Parse(user.PasswordHash);
      if (pbkdf2.IsMatch(password))
      {
        return user;
      }
    }

    return null;
  }

  private static string Normalize(string username) => username.Trim().ToUpper();
}
