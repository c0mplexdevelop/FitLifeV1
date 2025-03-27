using FitLife.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FitLife.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        User user = new()
        {
            Id = -1,
            Email = "test@test.com",
            NormalizedEmail = "test@test.com".ToUpper(),
            FirstName = "John",
            MiddleName = null,
            LastName = "Doe",
            Sex = Models.User.Enum.Sex.Male,
            DateOfBirth = DateOnly.MinValue,
            Username = "John Doe"
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test!23");
        modelBuilder.Entity<User>().HasData(user);

        base.OnModelCreating(modelBuilder);

    }
}
