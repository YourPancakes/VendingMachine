using FluentValidation;
using VendingMachine.Application.DTOs;

namespace VendingMachine.Application.Validators;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");
        
        RuleFor(x => x.BrandId)
            .GreaterThan(0).WithMessage("Brand ID must be greater than 0");
        
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative");
    }
} 