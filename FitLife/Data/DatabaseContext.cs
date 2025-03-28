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
            NormalizedUserName = "JohnDoe".ToUpper(),
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
