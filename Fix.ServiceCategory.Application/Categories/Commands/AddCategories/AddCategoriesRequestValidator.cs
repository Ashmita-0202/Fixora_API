using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Fix.Categories.Application.Categories.Commands.AddCategories
{
    public class AddCategoriesRequestValidator : AbstractValidator<AddCategoriesCommand>
    {
        public AddCategoriesRequestValidator()
        {
            RuleFor(x => x.categoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => x.iconUrl)
                .Must(BeAValidUrl).WithMessage("Icon URL must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.iconUrl));

            RuleFor(x => x.baseServiceCharge)
                .GreaterThanOrEqualTo(0).WithMessage("Base service charge cannot be negative.");
        }

        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }

}
