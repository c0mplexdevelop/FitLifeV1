using FitLife.Models.Exercises;
using FitLife.Models.Intermediary.Interfaces;

namespace FitLife.Models.Intermediary;

public class UserExerciseHistory : IIntermediaryBase
{
    public int Id { get; set; } // Primary key, auto-incremented
    // Foreign key to the UserTable
    public int UserId { get; set; }

    // Foreign key to the ExerciseTable
    public string ExerciseId { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;
    // Navigation properties
    public virtual User.User User { get; set; } = null!;
    public virtual Exercise Exercise { get; set; } = null!;
}
