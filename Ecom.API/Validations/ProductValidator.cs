using Ecom.Core.DTO_s;
using FluentValidation;

namespace Ecom.API.Validations
{
    public class AddProductValidator : AbstractValidator<AddProductDTO>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(150).WithMessage("Product Name must not exceed 150 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product Description is required.");

            RuleFor(p => p.NewPrice)
                .GreaterThan(0).WithMessage("Product Price must be greater than zero.");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Product must belong to a valid Category.");
        }
    }

    public class UpdateProductValidator : AbstractValidator<UpdateProductDTO>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("Invalid Product ID.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(150).WithMessage("Product Name must not exceed 150 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product Description is required.");

            RuleFor(p => p.NewPrice)
                .GreaterThan(0).WithMessage("Product Price must be greater than zero.");

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("Product must belong to a valid Category.");
        }
    }
}
