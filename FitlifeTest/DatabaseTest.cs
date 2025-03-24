using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using FitLife.Models.User.Enum;
using Moq;

namespace FitlifeTest;

public class DatabaseTest
{
    [Fact]
    public async Task GetByIdAsync_Returns_ValidUser()
    {
        //Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        User expectedUser = new()
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
}
