using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Queries.GetBookingById
{
    public class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
    {
        public GetBookingByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Booking id is required.");
        }
    }
}
