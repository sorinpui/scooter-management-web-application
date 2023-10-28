using FluentValidation;
using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private const string _empty = "This field is empty.";

    public LoginRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_empty);

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_empty);
    }
}
