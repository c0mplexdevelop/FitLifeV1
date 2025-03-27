using FitLife.Models.User;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Data;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            [
                new()
                {
                    Id = -1,
                    Email = "test@test.com",
                    FirstName = "John",
                    MiddleName = null,
                    LastName = "Doe",
                    Sex = Models.User.Enum.Sex.Male,
                    DateOfBirth = DateOnly.MinValue,
                    Username = "John Doe",
                    Password = "Test!23"
                }
            ]
        );

        base.OnModelCreating(modelBuilder);

    }
}
