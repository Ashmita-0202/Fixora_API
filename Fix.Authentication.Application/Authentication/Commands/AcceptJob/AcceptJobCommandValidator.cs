using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.AcceptJob
{
    public class AcceptJobCommandValidator : AbstractValidator<AcceptJobCommand>
    {
        public AcceptJobCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Booking id is required.");
        }
    }
}
