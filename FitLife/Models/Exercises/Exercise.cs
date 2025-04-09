using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises;

public class Exercise
{
    public string Id { get; set; } = string.Empty;
    public string Name = string.Empty;
    //public string Description = string.Empty;
    public string Type = string.Empty;
    public ExerciseDifficulty Difficulty;
    public string TargetMuscleGroup = string.Empty;
    public string EquipmentNeeded = string.Empty;
    public int Reps { get; set; }
    public int DurationInSeconds { get; set; }
    public int Sets { get; set; }

}
