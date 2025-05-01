using Microsoft.ML.Data;

namespace FitLife.Models.Exercises;

public class ExercisePrediction
{
    public string ExerciseID { get; set; } = string.Empty;

    [ColumnName("PredictedLabel")]
    public bool PredictedLabel { get; set; }
    public float Score { get; set; }
    public float Probability { get; set; }

    public override string ToString()
    {
        return $"ExerciseID: {ExerciseID}, PredictedLabel: {PredictedLabel}, Score: {Score}, Probability: {Probability}";
    }
}
