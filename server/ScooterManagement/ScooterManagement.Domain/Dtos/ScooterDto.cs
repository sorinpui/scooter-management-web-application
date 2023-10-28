namespace ScooterManagement.Domain.Dtos;

public class ScooterDto
{
    public string Name { get; set; }
    public int MaxSpeed { get; set; }
    public double RentalPrice { get; set; }
    public int? UserId { get; set; }
}
