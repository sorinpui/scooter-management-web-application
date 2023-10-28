using Microsoft.EntityFrameworkCore;
using ScooterManagement.Domain.Entites;
using ScooterManagement.Domain.Interfaces;

namespace ScooterManagement.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ScooterManagementDbContext _context;

    public UserRepository(ScooterManagementDbContext scooterManagementDbContext)
    {
        _context = scooterManagementDbContext;
    }

    public async Task CreateUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
    }
}
