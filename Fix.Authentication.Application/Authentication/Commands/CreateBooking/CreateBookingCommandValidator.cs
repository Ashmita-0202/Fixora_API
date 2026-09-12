using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.ServiceCategoryId).NotEmpty().WithMessage("Please select a service.");
            RuleFor(x => x.ScheduledDate).NotEmpty().WithMessage("Please select a date.");
            RuleFor(x => x.ScheduledTime).NotEmpty().WithMessage("Please select a time slot.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Service address is required.");
        }
    }
}
