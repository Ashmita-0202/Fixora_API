using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.CancelBooking
{
    public class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
    {
        public CancelBookingCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Booking id is required.");
        }
    }
}
