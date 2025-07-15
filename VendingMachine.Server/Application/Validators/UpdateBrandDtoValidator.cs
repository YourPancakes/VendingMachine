using FluentValidation;
using VendingMachine.Application.DTOs;

namespace VendingMachine.Application.Validators;

public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Brand ID must be greater than 0");
            
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Brand name is required")
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters");
    }
} 