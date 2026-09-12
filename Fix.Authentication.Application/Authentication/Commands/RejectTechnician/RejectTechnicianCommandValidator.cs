using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.RejectTechnician
{
    public class RejectTechnicianCommandValidator : AbstractValidator<RejectTechnicianCommand>
    {
        public RejectTechnicianCommandValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty().WithMessage("Technician profile id is required.");
        }
    }
}
