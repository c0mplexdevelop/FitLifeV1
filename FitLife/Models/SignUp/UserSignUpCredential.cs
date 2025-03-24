using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.SignUp;

public class UserSignUpCredential
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(16, MinimumLength = 6)]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(32, MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*()]).+$")]
    public string Password { get; set; } = null!;
}
