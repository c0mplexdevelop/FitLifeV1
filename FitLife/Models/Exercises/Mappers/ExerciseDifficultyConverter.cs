using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises.Mappers;

public class ExerciseDifficultyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        return Enum.TryParse<ExerciseDifficulty>(text, ignoreCase: true, out var difficulty);
    }
}
