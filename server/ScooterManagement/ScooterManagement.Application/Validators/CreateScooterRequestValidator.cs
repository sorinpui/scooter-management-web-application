using FluentValidation;
using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Application.Validators;

public class CreateScooterRequestValidator : AbstractValidator<CreateScooterRequest>
{
    public CreateScooterRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("This field is empty.");
        RuleFor(x => x.MaxSpeed).LessThan(60).WithMessage("The maximum allowed speed for a scooter is 60.");
        RuleFor(x => x.RentalPrice).InclusiveBetween(10, 100).WithMessage("Price must be between 10 and 100");
    }
}
