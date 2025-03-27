using FitLife.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FitLife.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<User> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("Users");
        base.OnModelCreating(builder);
    }

    public void SeedDataAsync()
    {
        User? user = Set<User>().Find(-1);
        if (user != null)
        {
            return;
        }
        user = new()
        {
            Id = -1,
            Email = "test@test.com",
            NormalizedEmail = "test@test.com".ToUpper(),
            FirstName = "John",
            MiddleName = null,
            LastName = "Doe",
            Sex = FitLife.Models.User.Enum.Sex.Male,
            DateOfBirth = DateOnly.MinValue,
            UserName = "JohnDoe",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test!23");
        Set<User>().Add(user);

        Database.OpenConnection();
        try
        {
            Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] ON");
            SaveChanges();
            Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF");
        }
        finally
        {
            Database.CloseConnection();
        }


    }
}
