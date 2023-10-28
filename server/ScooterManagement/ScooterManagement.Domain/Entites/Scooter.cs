using System.ComponentModel.DataAnnotations;

namespace ScooterManagement.Domain.Entites;

public class Scooter : EntityBase
{
    public override int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public int MaxSpeed { get; set; }

    [Required]
    public double RentalPrice { get; set; }

    public int? UserId { get; set; }
    public User User { get; set; }
}
