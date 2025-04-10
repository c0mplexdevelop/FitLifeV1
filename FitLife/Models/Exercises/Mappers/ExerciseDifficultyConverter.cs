using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises.Mappers;

public class ExerciseDifficultyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        if (Enum.TryParse<ExerciseDifficulty>(text, ignoreCase: true, out var difficulty))
        {
            return difficulty;
        }

        throw new Exception();
    }
}
