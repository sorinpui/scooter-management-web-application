namespace ScooterManagement.Domain.Requests;

public class CreateScooterRequest
{
    public string Name { get; set; }
    public int MaxSpeed { get; set; }
    public double RentalPrice { get; set; }
}
