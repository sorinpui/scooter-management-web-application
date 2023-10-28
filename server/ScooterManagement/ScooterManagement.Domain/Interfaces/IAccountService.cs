using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Domain.Interfaces;

public interface IAccountService
{
    Task RegisterUserAsync(RegisterRequest request);
    Task<string> LoginUserAsync(LoginRequest request);
}
