using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace FitLife.Auth;

public class AuthService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> SignInUser(UserLoginCredential userLoginCredential)
    {
        User? _applicationUser;
        if (IsIdentifierEmailFormat(userLoginCredential.LoginIdentifier))
        {
            _applicationUser = await _userManager.FindByEmailAsync(userLoginCredential.LoginIdentifier);
        }
        else
        {
            _applicationUser = await _userManager.FindByNameAsync(userLoginCredential.LoginIdentifier);
        }

        if (_applicationUser == null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.PasswordSignInAsync(_applicationUser, userLoginCredential.Password, isPersistent : true, false);
    }

    public static bool IsIdentifierEmailFormat(string identifier)
    {
        var emailRegex = new Regex(@"^[A-Za-z0-9!@#$%^&*()|{}~^_\-+=]+@[A-Za-z0-9-]+(?:\.[a-zA-Z0-9-]+)*");
        return emailRegex.IsMatch(identifier);
    }
}
