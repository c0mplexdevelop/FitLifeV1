using FitLife.Data;
using FitLife.Data.Repository;
using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using FitLife.Models.User.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FitlifeTest;

public class DatabaseTest
{

    User expectedUser = new()
    {
        Id = -1,
        Email = "test@test.com",
        FirstName = "John",
        MiddleName = null,
        LastName = "Doe",
        Sex = Sex.Male,
        DateOfBirth = DateOnly.MinValue,
        Username = "John Doe"
    };

    private User ReturnExpectedUserWithHashedPassword()
    {
        expectedUser.PasswordHash = new PasswordHasher<User>().HashPassword(expectedUser, "Test!23");
        return expectedUser;
    }

    [Fact]
    public async Task GetByIdAsync_Returns_ValidUser()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        User expectedUser = ReturnExpectedUserWithHashedPassword();

        mockUserRepository.Setup(repo => repo.GetByIdAsync(-1)).ReturnsAsync(expectedUser);

        //Act
        var result = await mockUserRepository.Object.GetByIdAsync(-1);

        //Assert
        Assert.Equal(expectedUser, result);
    }

    [Fact]
    public async Task GetByIdAsync_Returns_Null()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        User? expectedUser = null;
        mockUserRepository.Setup(repo => repo.GetByIdAsync(-1)).ReturnsAsync(expectedUser);
        //Act
        var result = await mockUserRepository.Object.GetByIdAsync(-1);
        //Assert
        Assert.Equal(expectedUser, result);
    }

    [Fact]
    public async Task AddAsync_Should_Add_User_To_DbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Used to Guarantee new database for each test, preventing same value inserts
            .Options;

        using var context = new DatabaseContext(options);
        var userRepository = new UserRepository(context);
        
        User expectedUser = ReturnExpectedUserWithHashedPassword();


        // Act
        await userRepository.AddAsync(expectedUser);
        await userRepository.SaveChangesAsync();

        // Assert
        Assert.Single(context.Users);
    }

    [Fact]
    public async Task AddAsyncRange_Should_Add_Collection_To_DbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new DatabaseContext(options);
        var userRepository = new UserRepository(context);

        User expectedUser = ReturnExpectedUserWithHashedPassword();


        // Act
        await userRepository.AddRangeAsync(new List<User> { expectedUser });
        await userRepository.SaveChangesAsync();

        // Assert
        Assert.NotEmpty(context.Users);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDelete_User()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DatabaseContext>().
            UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new DatabaseContext(options);
        var userRepository = new UserRepository(context);

        User expectedUser = ReturnExpectedUserWithHashedPassword();


        await userRepository.AddAsync(expectedUser);
        await userRepository.SaveChangesAsync();

        // Act
        userRepository.DeleteAsync(expectedUser);
        await userRepository.SaveChangesAsync();

        // Assert
        Assert.Empty(context.Users);
    }
}
