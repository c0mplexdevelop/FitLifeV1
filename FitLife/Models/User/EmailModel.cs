using System.ComponentModel.DataAnnotations;

namespace FitLife.Models.User;

public class EmailModel : IValidatableObject
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email format.")]
    public string NewEmail { get; set; } = string.Empty;

    public string OldEmail { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(!string.IsNullOrEmpty(NewEmail) && 
            !string.IsNullOrEmpty(OldEmail) && 
            NewEmail.Equals(OldEmail, StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult("New email cannot be the same as the old email.", [ nameof(NewEmail) ]);
        }
    }
}
