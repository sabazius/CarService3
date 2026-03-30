using CarService3.Models.Requests;
using FluentValidation;

namespace CarService3.Host.Validators
{
    public class AddCustomerValidator
        : AbstractValidator<AddCustomerRequest>
    {
        public AddCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email is required.")
                .MaximumLength(100)
                .WithMessage("Email must not exceed 100 characters.");
            RuleFor(x => x.Years)
                .InclusiveBetween(1980, 2030);
        }
    }
}
