using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.SignUp;

public class UserSignUpCredential
{
    private const string PASSWORD_REGEX_ERROR_MESSAGE = "Passwords must contain an Uppercase character, lowercase character, and a special character";
    private const string PASSWORD_LENGTH_ERROR_MESSAGE = "Passwords must be between 6 to 32 characters long.";
    private const string USERNAME_ERROR_MESSAGE = "Username must be between 6 to 16 characters long.";
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(16, MinimumLength = 6, ErrorMessage = USERNAME_ERROR_MESSAGE)]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(32, MinimumLength = 6, ErrorMessage = PASSWORD_LENGTH_ERROR_MESSAGE)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*()]).+$", ErrorMessage = PASSWORD_REGEX_ERROR_MESSAGE)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password are not identical!")]
    public string ConfirmPassword { get; set; } = null!;
}
