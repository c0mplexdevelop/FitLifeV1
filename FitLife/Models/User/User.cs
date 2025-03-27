using FitLife.Models.User.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.User;

public class User
{

    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(64)]
    public string FirstName { get; set; } = null!;

    [StringLength(64)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(64)]
    public string LastName { get; set; } = null!;

    [Required]
    public Sex Sex { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 6)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(32, MinimumLength = 8)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*()]).+$")]
    /*
     * To Future Maintainers, this regex means:
     * Checks if there is at least one uppercase letter and one special character in the password.
     * Hab fun.
     */
    public string Password { get; set; } = null!;
}
