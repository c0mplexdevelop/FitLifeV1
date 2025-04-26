using FitLife.Models.Exercises.Params;

namespace FitLife.Services;

public class FitnessDataService
{
    public static Dictionary<string, Dictionary<string, ExerciseParams>> GetBaseRanges()
    {
        var baseRanges = new Dictionary<string, Dictionary<string, ExerciseParams>>
        {
            ["Strength"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((2,3),(10,15),0)
                ["Beginner"] = new ExerciseParams { Sets = (2, 3), Reps = (10, 15), DurationMinutes = null },
                // Python: ((3,4),(8,12),0)
                ["Intermediate"] = new ExerciseParams { Sets = (3, 4), Reps = (8, 12), DurationMinutes = null,  },
                // Python: ((3,5),(4,8),0)
                ["Advanced"] = new ExerciseParams { Sets = (3, 5), Reps = (4, 8), DurationMinutes = null }
            },
            ["Cardio"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((1,1),(0,0), (15,30))
                ["Beginner"] = new ExerciseParams { Sets = (1, 1), Reps = null, DurationMinutes = (15, 30) },
                // Python: ((1,1),(0,0),(25,45))
                ["Intermediate"] = new ExerciseParams { Sets = (1, 1), Reps = null, DurationMinutes = (25, 45) },
                // Python: ((1,1),(0,0),(30,60))
                ["Advanced"] = new ExerciseParams { Sets = (1, 1), Reps = null, DurationMinutes = (30, 60) }
            },
            ["Flexibility"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((1,2),(0,0),(5,10))
                ["Beginner"] = new ExerciseParams { Sets = (1, 2), Reps = null, DurationMinutes = (5, 10) },
                // Python: ((2,3),(0,0),(8,15))
                ["Intermediate"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (8, 15) },
                // Python: ((2,3),(0,0),(10,15))
                ["Advanced"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (10, 15) }
            },
            ["Mobility"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((1,2),(0,0),(5,10))
                ["Beginner"] = new ExerciseParams { Sets = (1, 2), Reps = null, DurationMinutes = (5, 10) },
                // Python: ((2,3),(0,0),(8,15))
                ["Intermediate"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (8, 15) },
                // Python: ((2,3),(0,0),(10,15))
                ["Advanced"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (10, 15) }
            },
            ["Balance"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((2,3),(0,0),(5,10))
                ["Beginner"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (5, 10) },
                // Python: ((2,3),(0,0),(8,15))
                ["Intermediate"] = new ExerciseParams { Sets = (2, 3), Reps = null, DurationMinutes = (8, 15) },
                // Python: ((3,4),(0,0),(10,15))
                ["Advanced"] = new ExerciseParams { Sets = (3, 4), Reps = null, DurationMinutes = (10, 15) }
            },
            ["Core"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((2,3),(10,20),0)
                ["Beginner"] = new ExerciseParams { Sets = (2, 3), Reps = (10, 20), DurationMinutes = null },
                // Python: ((3,4),(15,25),0)
                ["Intermediate"] = new ExerciseParams { Sets = (3, 4), Reps = (15, 25), DurationMinutes = null },
                // Python: ((3,4),(15,30),0)
                ["Advanced"] = new ExerciseParams { Sets = (3, 4), Reps = (15, 30), DurationMinutes = null }
            },
            ["Power"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((2,3),(5,8),0)
                ["Beginner"] = new ExerciseParams { Sets = (2, 3), Reps = (5, 8), DurationMinutes = null },
                // Python: ((3,4),(3,6),0)
                ["Intermediate"] = new ExerciseParams { Sets = (3, 4), Reps = (3, 6), DurationMinutes = null },
                // Python: ((3,5),(1,5),0)
                ["Advanced"] = new ExerciseParams { Sets = (3, 5), Reps = (1, 5), DurationMinutes = null }
            },
            ["Plyometrics"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((2,3),(5,8),0)
                ["Beginner"] = new ExerciseParams { Sets = (2, 3), Reps = (5, 8), DurationMinutes = null },
                // Python: ((3,4),(3,6),0)
                ["Intermediate"] = new ExerciseParams { Sets = (3, 4), Reps = (3, 6), DurationMinutes = null },
                // Python: ((3,5),(1,5),0)
                ["Advanced"] = new ExerciseParams { Sets = (3, 5), Reps = (1, 5), DurationMinutes = null }
            },
            ["Unknown"] = new Dictionary<string, ExerciseParams>
            {
                // Python: ((1,1),(10,10),10)
                ["Beginner"] = new ExerciseParams { Sets = (1, 1), Reps = (10, 10), DurationMinutes = null },
                // Python: ((1,1),(10,10),10)
                ["Intermediate"] = new ExerciseParams { Sets = (1, 1), Reps = (10, 10), DurationMinutes = null },
                // Python: ((1,1),(10,10),10)
                ["Advanced"] = new ExerciseParams { Sets = (1, 1), Reps = (10, 10), DurationMinutes = null }
            }
        };
        return baseRanges;
    }

    // Example Usage:
    public static void Main(string[] args) // Or call from elsewhere
    {
        var ranges = GetBaseRanges();

        // Access example:
        ExerciseParams beginnerStrength = ranges["Strength"]["Beginner"];
        Console.WriteLine($"Beginner Strength: {beginnerStrength}");
        // Output: Beginner Strength: Sets: 2-3, Reps: 10-15, Duration: N/A, Rest: 0 sec

        ExerciseParams advancedCardio = ranges["Cardio"]["Advanced"];
        Console.WriteLine($"Advanced Cardio: {advancedCardio}");
        // Output: Advanced Cardio: Sets: 1-1, Reps: N/A, Duration: 30-60 min, Rest: N/A

        // Check if reps exist
        if (beginnerStrength.Reps.HasValue)
        {
            Console.WriteLine($"Beginner Strength Min Reps: {beginnerStrength.Reps.Value.Min}");
        }

        // Check if duration exists
        if (advancedCardio.DurationMinutes.HasValue)
        {
            Console.WriteLine($"Advanced Cardio Max Duration: {advancedCardio.DurationMinutes.Value.Max} minutes");
        }
    }
}