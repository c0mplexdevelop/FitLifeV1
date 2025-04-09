using CsvHelper;
using FitLife.Models.Exercises;
using FitLife.Models.Exercises.Mappers;
using System.Globalization;

namespace FitLife.Utilities;

public static class GetExercises
{
    public static CsvReader SetCSVReader(string csvPath)
    { 
        using var reader = new StreamReader(csvPath);
        if (reader == null)
        {
            throw new FileNotFoundException($"The file at {csvPath} was not found.");
        }

        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csvReader;
    }

    public static Exercise GetExercisesFromCsv(CsvReader reader)
    {
        reader.Context.RegisterClassMap<ExerciseCSVMapper>();
        var records = reader.GetRecords<Exercise>().ToList();
        if (records == null || records.Count == 0)
        {
            throw new InvalidOperationException("No records found in the CSV file.");
        }
        return records[0];
    }
}
