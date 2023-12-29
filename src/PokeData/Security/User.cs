namespace PokeData.Security;

internal record User
{
  public string Id { get; set; } = string.Empty;
  public string Username { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;

  public string? FullName { get; set; }
  public string? EmailAddress { get; set; }
  public string? PictureUrl { get; set; }
}
