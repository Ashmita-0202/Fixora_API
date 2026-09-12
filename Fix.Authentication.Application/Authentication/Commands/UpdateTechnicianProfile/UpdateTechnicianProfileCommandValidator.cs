using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile
{
    public class UpdateTechnicianProfileCommandValidator : AbstractValidator<UpdateTechnicianProfileCommand>
    {
        public UpdateTechnicianProfileCommandValidator()
        {
            RuleFor(x => x.Specialty).NotEmpty().WithMessage("Specialty is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.ExperienceYears).GreaterThanOrEqualTo(0).WithMessage("Experience years cannot be negative.");
        }
    }
}
