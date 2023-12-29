using FluentValidation;

namespace PokeData.Domain.Resources;

internal class SourceValidator : AbstractValidator<string>
{
  public SourceValidator()
  {
    RuleFor(x => x).NotEmpty()
      .MaximumLength(SourceUnit.MaximumLength);
  }
}
