using FitLife.Models.Exercises;

namespace FitLife.Models.Intermediary.Interfaces;

public interface IIntermediaryBase
{
    int UserId { get; set; }

    // Foreign key to the ExerciseTable
    string ExerciseId { get; set; }

    // Navigation properties
    User.User User { get; }
    Exercise Exercise { get; }
}
