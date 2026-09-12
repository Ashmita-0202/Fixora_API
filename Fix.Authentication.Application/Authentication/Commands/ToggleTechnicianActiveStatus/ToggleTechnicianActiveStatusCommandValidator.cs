using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleTechnicianActiveStatus
{
    public class ToggleTechnicianActiveStatusCommandValidator : AbstractValidator<ToggleTechnicianActiveStatusCommand>
    {
        public ToggleTechnicianActiveStatusCommandValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty().WithMessage("Technician profile id is required.");
        }
    }
}
