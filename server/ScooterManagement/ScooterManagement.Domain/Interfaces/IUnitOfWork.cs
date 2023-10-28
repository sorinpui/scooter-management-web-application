namespace ScooterManagement.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; set; }
    IScooterRepository ScooterRepository { get; set; }

    Task SaveAsync();
}
