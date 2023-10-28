using Microsoft.EntityFrameworkCore;
using ScooterManagement.Domain.Entites;

namespace ScooterManagement.DataAccess;

public class ScooterManagementDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Scooter> Scooters { get; set; }

    public ScooterManagementDbContext(DbContextOptions<ScooterManagementDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Role[] roles = new Role[]
        {
            new Role { Id = 1, Name = Domain.Enums.Role.Rider.ToString() },
            new Role { Id = 2, Name = Domain.Enums.Role.Admin.ToString() }
        };

        modelBuilder.Entity<Role>().HasData(roles);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}
