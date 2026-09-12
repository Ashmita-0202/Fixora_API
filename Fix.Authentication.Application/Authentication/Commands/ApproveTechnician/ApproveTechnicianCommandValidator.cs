using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.ApproveTechnician
{
    public class ApproveTechnicianCommandValidator : AbstractValidator<ApproveTechnicianCommand>
    {
        public ApproveTechnicianCommandValidator()
        {
            RuleFor(x => x.ProfileId).NotEmpty().WithMessage("Technician profile id is required.");
        }
    }
}
