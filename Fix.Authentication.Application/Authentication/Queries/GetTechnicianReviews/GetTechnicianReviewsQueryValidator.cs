using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Queries.GetTechnicianReviews
{
    public class GetTechnicianReviewsQueryValidator : AbstractValidator<GetTechnicianReviewsQuery>
    {
        public GetTechnicianReviewsQueryValidator()
        {
            RuleFor(x => x.TechnicianId).NotEmpty().WithMessage("Technician id is required.");
        }
    }
}
