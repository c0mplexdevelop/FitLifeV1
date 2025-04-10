using CsvHelper.Configuration;
using CsvHelper;
using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises.Mappers;

public sealed class ExerciseCSVMapper : ClassMap<Exercise>
{
    public ExerciseCSVMapper()
    {
        Map(m => m.Id).Name("ExerciseID");
        Map(m => m.Name).Name("Exercise Name");
        Map(m => m.Type).Name("Exercise Type");
        Map(m => m.TargetMuscleGroup).Name("Exercise Target Muscle Group");
        Map(m => m.EquipmentNeeded).Name("Equipment Needed");
        Map(m => m.Reps).Name("Reps");
        Map(m => m.Duration).Name("Duration");

        Map(m => m.Difficulty).Name("Exercise Difficulty") //Map the difficulty to enum
            .TypeConverter<ExerciseDifficultyConverter>();

    }
}
