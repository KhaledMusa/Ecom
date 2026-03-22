using Ecom.Core.DTO_s;
using FluentValidation;

namespace Ecom.API.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category Name is required.")
                .MaximumLength(100).WithMessage("Category Name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Category Description is required.")
                .MaximumLength(500).WithMessage("Category Description must not exceed 500 characters.");
        }
    }

    public class UpdateCategoryValidator : AbstractValidator<UpdatecategoryDTO>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("Invalid Category ID.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category Name is required.")
                .MaximumLength(100).WithMessage("Category Name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Category Description is required.")
                .MaximumLength(500).WithMessage("Category Description must not exceed 500 characters.");
        }
    }
}
