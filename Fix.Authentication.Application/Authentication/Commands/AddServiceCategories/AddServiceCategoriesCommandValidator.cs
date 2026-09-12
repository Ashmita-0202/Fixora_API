using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.AddServiceCategories
{
    public class AddServiceCategoriesCommandValidator : AbstractValidator<AddServiceCategoriesCommand>
    {
        public AddServiceCategoriesCommandValidator()
        {
            RuleFor(x => x.categoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.baseServiceCharge)
                .GreaterThan(0).WithMessage("Base service charge must be greater than 0.");
        }
    }
}
