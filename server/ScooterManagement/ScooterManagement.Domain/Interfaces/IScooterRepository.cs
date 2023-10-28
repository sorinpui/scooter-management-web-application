using ScooterManagement.Domain.Entites;

namespace ScooterManagement.Domain.Interfaces;

public interface IScooterRepository
{
    Task<List<Scooter>> GetScootersAsync();
    Task CreateScooterAsync(Scooter scooter);
    Task<Scooter?> GetScooterByIdAsync(int id);
    Task DeleteScooterAsync(int scooterId);

}
