using Microsoft.ML.Data;

namespace FitLife.Models.Exercises;

public class ExercisePrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedLabel { get; set; }
    public float Score { get; set; }
    public float Probability { get; set; }
}
