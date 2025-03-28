using Xunit;
using Moq;
using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using FitLife.Models.User.Enum;
using FitLife.Auth;


namespace FitlifeTest;

public class UserAuthTest
{
    User testUser = new()
    {
        Id = -1,
        Email = "test@test.com",
        FirstName = "John",
        MiddleName = null,
        LastName = "Doe",
        Sex = Sex.Male,
        DateOfBirth = DateOnly.MinValue,
        Username = "John Doe",
        Password = "Test!23"
    };
    [Fact]
    public async Task LoginAsyncUsingUsername_ReturnsTrue_WhenCredentialsAreCorrect()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User> { testUser });

        var authService = new AuthService(mockUserRepository.Object);

        var testCredentials = new UserLoginCredential
        {
            LoginIdentifier = "John Doe",
            Password = "Test!23"
        };

        // Act
        var result = await authService.LoginAsync(testCredentials);

        // Assert
        Assert.Equal(testUser, result);
    }

    [Fact]
    public async Task LoginAsyncUsingEmail_ReturnsTrue_WhenCredentialsAreCorrect()
    {
        // Arrange
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User> { testUser });

        var authService = new AuthService(mockUserRepository.Object);

        var testCredentials = new UserLoginCredential
        {
            LoginIdentifier = "test@test.com",
            Password = "Test!23"
        };

        // Act
        var result = await authService.LoginAsync(testCredentials);

        // Assert
        Assert.Equal(testUser, result);
    }

    [Fact]
    public async Task LoginAsyncUsingUsername_ReturnsNull_WhenPasswordisIncorrect()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User> { testUser });

        var authService = new AuthService(mockUserRepository.Object);

        var testCredentials = new UserLoginCredential
        {
            LoginIdentifier = "John Doe",
            Password = "Test!24"
        };

        // Act
        var result = await authService.LoginAsync(testCredentials);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsyncUsingUsername_ReturnsNull_WhenUsernameIsIncorrect()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();

        mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User> { testUser });

        var authService = new AuthService(mockUserRepository.Object);

        var testCredentials = new UserLoginCredential
        {
            LoginIdentifier = "John Doe1",
            Password = "Test!23"
        };

        // Act
        var result = await authService.LoginAsync(testCredentials);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsyncUsingEmail_ReturnsNull_WhenEmailIsIncorrect()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User> { testUser });
        var authService = new AuthService(mockUserRepository.Object);
        var testCredentials = new UserLoginCredential
        {
            LoginIdentifier = "tesst@gmail.com",
            Password = "Test!23"
        };

        // Act
        var result = await authService.LoginAsync(testCredentials);

        // Assert
        Assert.Null(result);

    }
}