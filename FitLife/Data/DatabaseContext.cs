using FitLife.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FitLife.Models.Exercises;
using FitLife.Utilities;


namespace FitLife.Data;

public class DatabaseContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<User> Accounts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    private ILogger<DatabaseContext>? _logger;

    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger) : base(options)
    {
        _logger = logger;
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("Users");

        builder.Entity<Exercise>().Property(e => e.Id).ValueGeneratedOnAdd();
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
            NormalizedUserName = "JohnDoe".ToUpper(),
            SecurityStamp = Guid.NewGuid().ToString()
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Test!23");
        Set<User>().Add(user);

        if(!Set<Exercise>().Any())
        {
            var csvPath = Path.Combine(Directory.GetCurrentDirectory(), "ExerciseTable.csv");
            using var csvReader = GetExercises.SetCSVReader(csvPath);

            var records = GetExercises.GetExercisesFromCsv(csvReader);

            foreach (var record in records)
            {
                _logger?.LogInformation($"Exercise: {record.Name}, Type: {record.Type}, Target Muscle Group: {record.TargetMuscleGroup}, Equipment Needed: {record.EquipmentNeeded}, Reps: {record.Reps}, Duration: {record.Duration}, Difficulty: {record.Difficulty}");
                Set<Exercise>().Add(record);
            }
        }

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
