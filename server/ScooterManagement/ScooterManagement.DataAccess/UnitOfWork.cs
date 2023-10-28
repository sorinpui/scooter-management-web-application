using ScooterManagement.DataAccess.Repositories;
using ScooterManagement.Domain.Interfaces;

namespace ScooterManagement.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ScooterManagementDbContext _context;

    public IUserRepository UserRepository { get; set; }
    public IScooterRepository ScooterRepository { get; set; }

    public UnitOfWork(ScooterManagementDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(context);
        ScooterRepository = new ScooterRepository(context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
