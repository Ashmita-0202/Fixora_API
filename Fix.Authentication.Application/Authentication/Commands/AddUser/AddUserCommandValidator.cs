using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.AddUser
    
{
    public class AddUserCommandValidator:AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required.");
            RuleFor(x => x.Email)
     .NotEmpty()
     .WithMessage("Email is required")
     .EmailAddress()
     .WithMessage("Please enter a valid email address")
     .MaximumLength(100)
     .WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

            When(x => x.UserRole == Fix.Shared.Domain.Models.Entities.UserRole.Technician, () =>
            {
                RuleFor(x => x.Specialty).NotEmpty().WithMessage("Specialty is required for technician registration.");
                RuleFor(x => x.City).NotEmpty().WithMessage("City is required for technician registration.");
                RuleFor(x => x.ExperienceYears)
                    .NotNull()
                    .WithMessage("Years of experience is required for technician registration.");
                RuleFor(x => x.ExperienceYears)
                    .GreaterThanOrEqualTo(0)
                    .When(x => x.ExperienceYears.HasValue)
                    .WithMessage("Years of experience cannot be negative.");
            });

        }
    }
}
