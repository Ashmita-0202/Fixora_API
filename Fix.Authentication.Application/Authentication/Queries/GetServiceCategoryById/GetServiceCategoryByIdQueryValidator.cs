using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryValidator : AbstractValidator<GetServiceCategoryByIdQuery>
    {
        public GetServiceCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Service category id is required.");
        }
    }
}
