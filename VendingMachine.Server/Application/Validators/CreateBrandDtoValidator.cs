using FluentValidation;
using VendingMachine.Application.DTOs;

namespace VendingMachine.Application.Validators;

public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Brand name is required")
            .MaximumLength(100).WithMessage("Brand name cannot exceed 100 characters");
    }
} 