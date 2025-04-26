namespace FitLife.Models.Exercises.Params;

public struct ExerciseParams
{
    // Represents the (min, max) tuple for sets
    public (int Min, int Max) Sets { get; set; }

    // Represents the (min, max) tuple for reps. Nullable if not applicable (e.g., Cardio).
    public (int Min, int Max)? Reps { get; set; }

    // Represents the (min, max) tuple for duration (in minutes). Nullable if not applicable (e.g., Strength).
    public (int Min, int Max)? DurationMinutes { get; set; }

    // Represents the rest time in seconds. Nullable if not applicable (e.g., Cardio) or if 0 means "not specified".

    // Optional: Override ToString for easy printing/debugging
    public override string ToString()
    {
        string setsStr = $"Sets: {Sets.Min}-{Sets.Max}";
        string repsStr = Reps.HasValue ? $"Reps: {Reps.Value.Min}-{Reps.Value.Max}" : "Reps: N/A";
        string durationStr = DurationMinutes.HasValue ? $"Duration: {DurationMinutes.Value.Min}-{DurationMinutes.Value.Max} min" : "Duration: N/A";

        return $"{setsStr}, {repsStr}, {durationStr}";
    }
}