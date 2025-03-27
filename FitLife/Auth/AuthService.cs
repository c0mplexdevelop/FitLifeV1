using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace FitLife.Auth;

public class AuthService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private ILogger<AuthService>? _logger;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService>? logger = null)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<SignInResult> SignInUser(UserLoginCredential userLoginCredential)
    {
        User? _applicationUser;
        if (IsIdentifierEmailFormat(userLoginCredential.LoginIdentifier))
        {
            _logger.LogInformation("Identifier is in email format.");
            _applicationUser = await _userManager.FindByEmailAsync(userLoginCredential.LoginIdentifier);
        }
        else
        {
            _logger.LogInformation("Identifier is in username format.");
            _applicationUser = await _userManager.FindByNameAsync(userLoginCredential.LoginIdentifier);
        }


        if (_applicationUser == null)
        {
            _logger.LogWarning("User not found.");
            return SignInResult.Failed;
        }

        _logger.LogInformation($"User found: {_applicationUser.UserName} {_applicationUser.NormalizedUserName}");

        return await _signInManager.PasswordSignInAsync(_applicationUser, userLoginCredential.Password, isPersistent : false, false);
    }

    public static bool IsIdentifierEmailFormat(string identifier)
    {
        var emailRegex = new Regex(@"^[A-Za-z0-9!@#$%^&*()|{}~^_\-+=.]+@[A-Za-z0-9-]+(\.[a-zA-Z0-9-]+)");
        return emailRegex.IsMatch(identifier);
    }
}
