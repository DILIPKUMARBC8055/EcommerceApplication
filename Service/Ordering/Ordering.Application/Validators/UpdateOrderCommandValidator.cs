using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("{Id} is required");
            RuleFor(o => o.UserName)
               .NotEmpty()
               .WithMessage("{UserName} can't be empty")
               .MaximumLength(70)
               .WithMessage("{UserName} can't exceed 70 char");
            RuleFor(o => o.TotalPrice)
                .NotEmpty()
                .WithMessage("{TotalPrice} can't be empty")
                .GreaterThan(-1)
                .WithMessage("{TotalPrice} can't be negative");
            RuleFor(o => o.EmailAddress)
                .NotEmpty()
                .WithMessage("{EmailAddress} is required");
            RuleFor(o => o.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{FirstName} is required");
            RuleFor(o => o.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("{LastName} is required");
        }
    }
}
