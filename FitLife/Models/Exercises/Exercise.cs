using FitLife.Models.Exercises.Enums;
using FitLife.Models.Intermediary;

namespace FitLife.Models.Exercises;

public class Exercise
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    //public string Description = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ExerciseDifficulty Difficulty { get; set; } // Convert to string when feed to model
    public string TargetMuscleGroup { get; set; } = string.Empty;
    public bool EquipmentNeeded { get; set; } // Convert to float/int when feed to model
    public int MinReps { get; set; }
    public int MaxReps { get; set; }
    public int MinDurationMinutes { get; set; } // Duration in Minutes
    public int MaxDurationMinutes { get; set; }
    public string? Equipments { get; set; }
    public int MinSets { get; set; }
    public int MaxSets { get; set; }

    // Navigation properties
    public virtual ICollection<UserExerciseSubscription> UserSubscriptions { get; set; } = new List<UserExerciseSubscription>();
}
