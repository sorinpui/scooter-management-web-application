using FluentValidation;

namespace ScooterManagement.Domain.Interfaces;

public interface IValidatorsFactory
{
    IValidator<T> GetValidator<T>();
}
