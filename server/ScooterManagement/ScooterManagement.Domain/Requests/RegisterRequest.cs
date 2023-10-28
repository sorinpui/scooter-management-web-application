using ScooterManagement.Domain.Enums;

namespace ScooterManagement.Domain.Requests;

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role RoleId { get; set; }
}
