using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises;

public class Exercise
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    //public string Description = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ExerciseDifficulty Difficulty { get; set; }
    public string TargetMuscleGroup { get; set; } = string.Empty;
    public string EquipmentNeeded { get; set; } = string.Empty;
    public string Reps { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Sets { get; set; } = string.Empty;

}
