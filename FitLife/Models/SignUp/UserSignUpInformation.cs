using FitLife.Models.User.Enum;
using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.SignUp;

public class UserSignUpInformation
{
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

    public int SelectedMonth { get; set; }
    public int SelectedDay { get; set; }
    public int SelectedYear { get; set; }
}
