using FluentValidation;

namespace PokeData.Domain.Resources;

public record JsonStringUnit
{
  public string Value { get; }

  public JsonStringUnit(string value)
  {
    Value = value.Trim();
    new JsonStringValidator().ValidateAndThrow(Value);
  }
}
