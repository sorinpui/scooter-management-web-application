using FluentValidation;
using ScooterManagement.Domain.Interfaces;

namespace ScooterManagement.Application.Services;

public class ValidationService : IValidationService
{
    private readonly IValidatorsFactory _validatorsFactory;

    public ValidationService(IValidatorsFactory validatorsFactory)
    {
        _validatorsFactory = validatorsFactory;
    }

    public async Task ValidateRequestAsync<T>(T request)
    {
        await _validatorsFactory.GetValidator<T>().ValidateAndThrowAsync(request);
    }
}