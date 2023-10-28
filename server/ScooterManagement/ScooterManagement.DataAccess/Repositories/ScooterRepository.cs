using Microsoft.EntityFrameworkCore;
using ScooterManagement.Domain.Entites;
using ScooterManagement.Domain.Interfaces;

namespace ScooterManagement.DataAccess.Repositories;

public class ScooterRepository : IScooterRepository
{
    private readonly ScooterManagementDbContext _context;

    public ScooterRepository(ScooterManagementDbContext context)
    {
        _context = context;
    }

    public async Task<List<Scooter>> GetScootersAsync()
    {
        return await _context.Scooters.ToListAsync();
    }

    public async Task CreateScooterAsync(Scooter scooter)
    {
        await _context.Scooters.AddAsync(scooter);
    }

    public async Task<Scooter?> GetScooterByIdAsync(int id)
    {
        return await _context.Scooters.Where(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task DeleteScooterAsync(int id)
    {
        Scooter scooterToDelete = await _context.Scooters.Where(s => s.Id == id).FirstOrDefaultAsync();

        _context.Scooters.Remove(scooterToDelete);
    }
}
