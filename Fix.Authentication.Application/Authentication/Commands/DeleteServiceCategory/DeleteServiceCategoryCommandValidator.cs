using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandValidator : AbstractValidator<DeleteServiceCategoryCommand>
    {
        public DeleteServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Service category id is required.");
        }
    }
}
