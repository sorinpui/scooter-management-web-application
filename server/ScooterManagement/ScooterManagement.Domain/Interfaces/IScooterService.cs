using ScooterManagement.Domain.Dtos;
using ScooterManagement.Domain.Requests;

namespace ScooterManagement.Domain.Interfaces;

public interface IScooterService
{
    Task<List<ScooterDto>> GetScootersAsync();
    Task CreateScooterAsync(CreateScooterRequest request);
    Task UpdateScooterAsync(int scooterId);
    Task DeleteScooterAsync(int scooterId);
    Task ReturnScooterAsync(int scooterId);
}
