using FluentValidation;
using ScooterManagement.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ScooterManagement.Application.Factories;

public class ValidatorsFactory : IValidatorsFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<T> GetValidator<T>()
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}
