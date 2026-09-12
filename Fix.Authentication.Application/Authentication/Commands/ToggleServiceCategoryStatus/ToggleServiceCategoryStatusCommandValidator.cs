using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus
{
    public class ToggleServiceCategoryStatusCommandValidator : AbstractValidator<ToggleServiceCategoryStatusCommand>
    {
        public ToggleServiceCategoryStatusCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Service category id is required.");
        }
    }
}
