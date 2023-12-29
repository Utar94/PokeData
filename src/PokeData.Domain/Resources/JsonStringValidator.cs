using FluentValidation;

namespace PokeData.Domain.Resources;

internal class JsonStringValidator : AbstractValidator<string>
{
  public JsonStringValidator()
  {
    RuleFor(x => x).NotEmpty();
  }
}
