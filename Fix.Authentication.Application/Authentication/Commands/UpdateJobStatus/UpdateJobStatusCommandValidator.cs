using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus
{
    public class UpdateJobStatusCommandValidator : AbstractValidator<UpdateJobStatusCommand>
    {
        public UpdateJobStatusCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().WithMessage("Booking id is required.");
            RuleFor(x => x.NewStatus).NotEmpty().WithMessage("New status is required.");
        }
    }
}
