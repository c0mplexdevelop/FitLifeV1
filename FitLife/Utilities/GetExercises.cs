using CsvHelper;
using FitLife.Models.Exercises;
using FitLife.Models.Exercises.Mappers;
using System.Globalization;

namespace FitLife.Utilities;

public static class GetExercises
{
    public static CsvReader SetCSVReader(string csvPath)
    { 
        var reader = new StreamReader(csvPath);
        if (reader == null)
        {
            throw new FileNotFoundException($"The file at {csvPath} was not found.");
        }

        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        csvReader.Context.RegisterClassMap<ExerciseCSVMapper>();

        return csvReader;
    }

    public static IEnumerable<Exercise> GetExercisesFromCsv(CsvReader reader)
    {
        var records = reader.GetRecords<Exercise>().ToList();
        if (records == null || records.Count == 0)
        {
            throw new InvalidOperationException("No records found in the CSV file.");
        }
        return records;
    }
}
