using System.ComponentModel.DataAnnotations;

namespace ScooterManagement.Domain.Entites;

public abstract class EntityBase
{
    [Key]
    public abstract int Id { get; set; }
}
