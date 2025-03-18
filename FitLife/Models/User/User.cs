using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.User;

public class User
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(64)]
    public string FirstName { get; set; } = null!;

    [MaxLength(64)]
    public string? MiddleName { get; set; }

    [Required]
    [MaxLength(64)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(16, MinimumLength = 6)]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
