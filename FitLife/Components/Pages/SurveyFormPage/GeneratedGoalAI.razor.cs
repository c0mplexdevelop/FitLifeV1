using FitLife.Data;
using FitLife.Models.Exercises;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace FitLife.Components.Pages.SurveyFormPage;

public partial class GeneratedGoalAI
{
    private const string MODEL_PATH = "AIModel.zip";

    private bool isLoading = true;

    private ITransformer? model;

    [Inject]
    private DatabaseContext dbContext { get; set; }

    private List<Exercise> exercises = new List<Exercise>();
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Task.Delay(TimeSpan.FromSeconds(4));

        MLContext mlContext = new MLContext();
        model = LoadModel(mlContext);
        exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();
        if (model == null)
        {
            Console.WriteLine("Model is null");
            return;
        }

        //TODO: Simulate data here
        var predictionEngine = mlContext.Model.CreatePredictionEngine<SurveyForm, ExercisePrediction>(model);
        isLoading = false;
    }

    public static ITransformer LoadModel(MLContext mlContext)
    {
        string modelPath = Path.Combine(Environment.CurrentDirectory, MODEL_PATH);
        ITransformer loadedModel = mlContext.Model.Load(modelPath, out DataViewSchema modelSchema);
        Console.WriteLine($"Model loaded from {modelPath}");

        return loadedModel;
    }
}
