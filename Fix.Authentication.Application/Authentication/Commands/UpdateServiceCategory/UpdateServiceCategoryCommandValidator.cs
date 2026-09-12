using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandValidator : AbstractValidator<UpdateServiceCategoryCommand>
    {
        public UpdateServiceCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Service category id is required.");

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.BaseServiceCharge)
                .GreaterThan(0).WithMessage("Base service charge must be greater than 0.");
        }
    }
}
