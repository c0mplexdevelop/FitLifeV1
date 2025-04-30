using FitLife.Data.Repository.Interface;
using FitLife.Models.State;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Text.RegularExpressions;

namespace FitLife.Auth;

public class AuthService
{

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private ILogger<AuthService> _logger;

    

    private static readonly int MIN_AGE_REQUIRED = 10;
    private static readonly int MAX_AGE_REQUIRED = 128; // Theoretical max lifespan is around 125, made it 128 to keep coomputer happy (bytes, multiple of 8)

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthService> logger)
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

    // TODO: Implement unit test for this method:
    public async Task<User?> RegisterUserAsync(UserSignUpState userState)
    {
   
        async Task<IdentityResult> CreateUserAsync()
        {
            var user = new User
            {
                UserName = userState.UserSignUpCredential.Username,
                Email = userState.UserSignUpCredential.Email,
                FirstName = userState.UserSignUpInformation.FirstName,
                MiddleName = userState.UserSignUpInformation.MiddleName,
                LastName = userState.UserSignUpInformation.LastName,
                Sex = userState.UserSignUpInformation.Sex,
                DateOfBirth = userState.UserSignUpInformation.DateOfBirth
            };
            var result = await _userManager.CreateAsync(user, userState.UserSignUpCredential.Password);
            if (!result.Succeeded)
            {
                _logger.LogCritical("Failed to create user.");
            }
            return result;
        }

        var yearToday = DateTime.Now;
        var birthYear = userState.UserSignUpInformation.DateOfBirth.ToDateTime(TimeOnly.MinValue);
        var age = yearToday.Year - birthYear.Year;

        if (yearToday < birthYear.AddYears(age))
        {
            age--;
        }

        if (age < MIN_AGE_REQUIRED)
        {
            _logger.LogError("User is below the minimum age requirement.");
            return null;
        } else if(age > MAX_AGE_REQUIRED)
        {
            _logger.LogError("User is above the maximum age requirement.");
            return null;
        }


        var result = await CreateUserAsync();
        if (!result.Succeeded)
        {
            _logger.LogCritical("Failed to create user.");
            foreach (var error in result.Errors)
            {
                _logger.LogError(error.Description);
            }
            return null; ;
        }

        var user = await _userManager.FindByEmailAsync(userState.UserSignUpCredential.Email);
        if (user == null)
        {
            _logger.LogCritical("Failed to find user.");
            return null;
        }

        return user;
    }

    public async Task SignOutUser()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<User> GetCurrentUser()
    {
        var user = await _userManager.GetUserAsync(_signInManager.Context.User);
        if (user == null)
        {
            _logger.LogError("User not found.");
            throw new Exception("User not found.");
        }
        return user;
    }

    public async Task<string> ReturnUserName()
    {
        var user = await GetCurrentUser();
        return user.UserName!;
    }

    public async Task<int> ReturnUserId()
    {
        var user = await GetCurrentUser();
        return user.Id;
    }
}
