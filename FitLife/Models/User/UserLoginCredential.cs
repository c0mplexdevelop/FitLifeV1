using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.User;

public class UserLoginCredential
{
    [Required]
    public string LoginIdentifier { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
