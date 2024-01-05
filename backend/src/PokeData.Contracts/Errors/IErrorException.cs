namespace PokeData.Contracts.Errors;

public interface IErrorException
{
  Error Error { get; }
}
