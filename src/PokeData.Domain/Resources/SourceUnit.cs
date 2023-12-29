using FluentValidation;

namespace PokeData.Domain.Resources;

public record SourceUnit
{
  public const int MaximumLength = 2048;

  public string Value { get; }

  public SourceUnit(string value)
  {
    Value = value.Trim();
    new SourceValidator().ValidateAndThrow(Value);
  }
}
