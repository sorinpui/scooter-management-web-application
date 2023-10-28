using ScooterManagement.Domain.Entites;

namespace ScooterManagement.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
}
