using CsvHelper.Configuration;
using CsvHelper;
using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises.Mappers;

public sealed class ExerciseCSVMapper : ClassMap<Exercise>
{
    public ExerciseCSVMapper()
    {
        Map(m => m.Id).Name("ExerciseID");
        Map(m => m.Name).Name("ExerciseName");
        Map(m => m.Type).Name("ExerciseType");
        Map(m => m.TargetMuscleGroup).Name("ExerciseTargetMuscleGroup");
        Map(m => m.EquipmentNeeded).Name("IsEquipmentNeeded");
        Map(m => m.MinReps).Name("MinReps")
            .TypeConverter<ExerciseRepsConverter>();
        Map(m => m.MaxReps).Name("MaxReps")
            .TypeConverter<ExerciseRepsConverter>();
        Map(m => m.MinDurationMinutes).Name("MinDurationMinutes")
            .TypeConverter<ExerciseDurationCoverter>();
        Map(m => m.MaxDurationMinutes).Name("MaxDurationMinutes")
            .TypeConverter<ExerciseDurationCoverter>();
        Map(m => m.Equipments).Name("Equipment Needed"); // This is a string, so no need to convert
        Map(m => m.MinSets).Name("MinSets");
        Map(m => m.MaxSets).Name("MaxSets");


        Map(m => m.Difficulty).Name("ExerciseDifficulty") //Map the difficulty to enum
            .TypeConverter<ExerciseDifficultyConverter>();

    }
}
