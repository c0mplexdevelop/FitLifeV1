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

    public override string ToString()
    {
        return $"FirstName: {FirstName}, MiddleName: {MiddleName}, LastName: {LastName}, Sex: {Sex.ToString()}, DateOfBirth: {DateOfBirth}";
    }
}
