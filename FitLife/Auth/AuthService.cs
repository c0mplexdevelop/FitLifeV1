using FitLife.Data.Repository.Interface;
using FitLife.Models.User;
using System.Text.RegularExpressions;

namespace FitLife.Auth;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> LoginAsyncUsingUsername(string username, string password)
    {
        var user = await _userRepository.GetAllAsync();
        return user.FirstOrDefault(x => x.Username == username && x.Password == password);
    }

    public async Task<User?> LoginAsyncUsingEmail(string email, string password)
    {
        var user = await _userRepository.GetAllAsync();
        return user.FirstOrDefault(x => x.Email == email && x.Password == password);
    }

    public async Task<User?> LoginAsync(UserLoginCredential userLoginCredential)
    {
        if(IsIdentifierEmailFormat(userLoginCredential.LoginIdentifier))
        {
            return await LoginAsyncUsingEmail(userLoginCredential.LoginIdentifier, userLoginCredential.Password);
        }
        return await LoginAsyncUsingUsername(userLoginCredential.LoginIdentifier, userLoginCredential.Password);
    }

    private bool IsIdentifierEmailFormat(string identifier)
    {
        var emailRegex = new Regex(@"^[A-Za-z0-9!@#$%^&*()|{}~^_\-+=]+@[A-Za-z0-9-]+(?:\.[a-zA-Z0-9-]+)*");
        return emailRegex.IsMatch(identifier);
    }
}
