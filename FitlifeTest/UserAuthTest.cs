using Xunit;
using Moq;
using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using FitLife.Models.User.Enum;
using FitLife.Auth;
using Microsoft.AspNetCore.Identity;


namespace FitlifeTest;

public class UserAuthTest
{
    [Fact]
    public void IsIdentifierEmailFormat_Returns_True_WithValidEmail()
    {
        // Arrange
        var testEmail = "test@test.com";

        // Act
        var result = AuthService.IsIdentifierEmailFormat(testEmail);

        // Assert
        Assert.True(result);
    }

    
}