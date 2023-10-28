namespace ScooterManagement.Domain.Interfaces;

public interface IValidationService
{
    Task ValidateRequestAsync<T>(T request);
}
