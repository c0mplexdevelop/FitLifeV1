using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;
using FitLife.Models.Exercises.Enums;

namespace FitLife.Models.Exercises.Mappers;

public class ExerciseDurationCoverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        if (text.Equals("n/a", StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        } else if(int.TryParse(text, out var duration))
        {
            return duration;
        }

        throw new Exception();
    }
}
