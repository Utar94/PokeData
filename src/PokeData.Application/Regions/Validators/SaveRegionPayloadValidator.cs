using FluentValidation;
using PokeData.Contracts.Regions;

namespace PokeData.Application.Regions.Validators;

internal class SaveRegionPayloadValidator : AbstractValidator<SaveRegionPayload>
{
  public SaveRegionPayloadValidator()
  {
    RuleFor(x => x.UniqueName).NotEmpty().MaximumLength(128);
    When(x => x.DisplayName != null, () => RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(128));
  }
}
