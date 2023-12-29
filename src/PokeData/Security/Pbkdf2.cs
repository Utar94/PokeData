using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PokeData.Security;

internal class Pbkdf2
{
  private const string Key = "PBKDF2";
  private const char Separator = ':';

  private readonly KeyDerivationPrf _algorithm;
  private readonly int _iterations;
  private readonly byte[] _salt;
  private readonly byte[] _hash;

  public Pbkdf2(string password, KeyDerivationPrf algorithm = KeyDerivationPrf.HMACSHA256, int iterations = 600000, int saltLength = 256 / 8, int? hashLength = null)
  {
    _algorithm = algorithm;
    _iterations = iterations;
    _salt = RandomNumberGenerator.GetBytes(saltLength);
    _hash = ComputeHash(password, hashLength ?? saltLength);
  }

  private Pbkdf2(KeyDerivationPrf algorithm, int iterations, byte[] salt, byte[] hash)
  {
    _algorithm = algorithm;
    _iterations = iterations;
    _salt = salt;
    _hash = hash;
  }

  public static Pbkdf2 Parse(string s)
  {
    string[] values = s.Split(Separator);
    if (values.Length != 5 || values.First() != Key)
    {
      throw new ArgumentException($"The value '{s}' is not a valid PBKDF2 password.", nameof(s));
    }

    return new Pbkdf2(Enum.Parse<KeyDerivationPrf>(values[1]), int.Parse(values[2]),
      Convert.FromBase64String(values[3]), Convert.FromBase64String(values[4]));
  }
  public static bool TryParse(string s, out Pbkdf2? pbkdf2)
  {
    try
    {
      pbkdf2 = Parse(s);
      return true;
    }
    catch (Exception)
    {
      pbkdf2 = null;
      return false;
    }
  }

  public bool IsMatch(string password) => _hash.SequenceEqual(ComputeHash(password));

  private byte[] ComputeHash(string password, int? length = null)
  {
    return KeyDerivation.Pbkdf2(password, _salt, _algorithm, _iterations, length ?? _hash.Length);
  }

  public override bool Equals(object? obj) => obj is Pbkdf2 pbkdf2 && pbkdf2.ToString() == ToString();
  public override int GetHashCode() => ToString().GetHashCode();
  public override string ToString() => string.Join(Separator, Key, _algorithm, _iterations,
    Convert.ToBase64String(_salt), Convert.ToBase64String(_hash));
}
