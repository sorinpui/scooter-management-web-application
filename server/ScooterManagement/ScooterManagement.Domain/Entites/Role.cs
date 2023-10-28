using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScooterManagement.Domain.Entites;

public class Role : EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
