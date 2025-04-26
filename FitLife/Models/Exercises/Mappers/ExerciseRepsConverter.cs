using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace FitLife.Models.Exercises.Mappers;

public class ExerciseRepsConverter : DefaultTypeConverter
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
        }
        else if (int.TryParse(text, out var reps))
        {
            return reps;
        }
        throw new Exception();
    }
}
