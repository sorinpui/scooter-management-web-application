using FluentValidation;
using ScooterManagement.Domain.Requests;
using System.Text.RegularExpressions;

namespace ScooterManagement.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private readonly string _empty = "This field is empty.";
    private readonly string _password = "This password is not strong enough.";

    public RegisterRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.FirstName)
            .NotEmpty().WithMessage(_empty);
        RuleFor(request => request.LastName)
            .NotEmpty().WithMessage(_empty);

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_empty)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_empty)
            .Must(BeStrong).WithMessage(_password);

        RuleFor(request => request.RoleId)
            .NotEmpty().WithMessage(_empty)
            .IsInEnum().WithMessage("There is no role associated with your input.")
            .NotEqual(Domain.Enums.Role.Admin).WithMessage("You cannot create an admin account.");
    }

    private bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }
}
