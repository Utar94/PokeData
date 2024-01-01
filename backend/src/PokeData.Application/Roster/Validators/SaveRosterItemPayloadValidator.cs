using FluentValidation;
using PokeData.Contracts.Roster;

namespace PokeData.Application.Roster.Validators;

internal class SaveRosterItemPayloadValidator : AbstractValidator<SaveRosterItemPayload>
{
  public SaveRosterItemPayloadValidator()
  {
    RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
    RuleFor(x => x.Category).MaximumLength(128);
  }
}
