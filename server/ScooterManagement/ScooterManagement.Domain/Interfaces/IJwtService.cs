namespace ScooterManagement.Domain.Interfaces;

public interface IJwtService
{
    public string CreateToken(int roleId, int userId);
    public int GetNameIdentifier();
}
