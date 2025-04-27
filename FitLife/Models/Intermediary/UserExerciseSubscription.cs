using System.ComponentModel.DataAnnotations;
using FitLife.Models.Exercises;

namespace FitLife.Models.Intermediary;

public class UserExerciseSubscription
{
    // Foreign key to the UserTable
    public int UserId { get; set; }

    // Foreign key to the ExerciseTable
    public string ExerciseId { get; set; } = string.Empty;

    // Navigation properties
    public virtual User.User User { get; set; } = null!;
    public virtual Exercise Exercise { get; set; } = null!;
}
