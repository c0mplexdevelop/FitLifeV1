using FitLife.Auth;
using FitLife.Data;
using FitLife.Models.Exercises;
using FitLife.Models.Intermediary;
using FitLife.Models.Survey;
using FitLife.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace FitLife.Components.Pages.SurveyFormPage;

public partial class GeneratedGoalAI
{
    [Inject]
    private SurveyService _surveyService { get; set; } = default!;
    private const string MODEL_PATH = "AIModel.zip";

    private bool isLoading = true;

    private ITransformer? model;

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    [Inject]
    private ILogger<GeneratedGoalAI> _logger { get; set; } = default!;

    [Inject]
    private AuthService _authService { get; set; } = default!;

    private List<Exercise> exercises = new List<Exercise>();
    private List<Exercise?> predictedExercise = new List<Exercise?>(); 
    protected override async Task OnInitializedAsync()
    {
        //surveyModel ??= new SurveyModel
        //{
        //    Age = 21,
        //    Height = 153,
        //    Weight = 56.5f,
        //    Gender = "Female",
        //    ActivityLevel = ActivityLevel.Active,
        //    StruggledPreviously = 1,
        //    FitnessGoal = "Muscle Gain",
        //    Exercises = [
        //        dbContext.Exercises.AsNoTracking()
        //            .Where(exercise => exercise.Id == "STR_016")
        //            .FirstOrDefault()
        //        ],
        //    Label = default
        //};
        SurveyModel surveyModel = _surveyService.SurveyModel;
        // Display surveyModel
        _logger.LogInformation($"SurveyModel: {surveyModel}");
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

        var modelInputs = exercises
            .Select(exercise => CreateModelInput(surveyModel, exercise))
            .ToList();
        IDataView dataView = mlContext.Data.LoadFromEnumerable(modelInputs);
        IDataView predictionView = model.Transform(dataView);
        List<ExercisePrediction> predictionResults = mlContext.Data
            .CreateEnumerable<ExercisePrediction>(predictionView, reuseRowObject: false)
            .ToList();

        var probabilityMap = predictionResults.ToDictionary(
            prediction => prediction.ExerciseID,
            prediction => prediction.Probability);

        var fiveExerciseMap = probabilityMap
            .OrderByDescending(pair => pair.Value)
            .Take(5)
            .ToList();
        //fiveExerciseMap.ForEach(pair =>
        //{
        //    _logger.LogInformation($"ExerciseID: {pair.Key}, Probability: {pair.Value}");
        //});
        // Above is for debug purposes
        predictedExercise = fiveExerciseMap.Select(pair => exercises.FirstOrDefault(exercise => exercise.Id == pair.Key))
            .Where(exercise => exercise != null)
            .ToList();

        isLoading = false;
    }

    public static ITransformer LoadModel(MLContext mlContext)
    {
        string modelPath = Path.Combine(Environment.CurrentDirectory, MODEL_PATH);
        ITransformer loadedModel = mlContext.Model.Load(modelPath, out DataViewSchema modelSchema);
        Console.WriteLine($"Model loaded from {modelPath}");

        return loadedModel;
    }

    public static ModelInput CreateModelInput(SurveyModel userInfo, Exercise exercise)
    {
        //var exerciseRepsRange = exercise.Reps.Split("-");
        var random = new Random();
        //var randomReps = random.Next(int.Parse(exerciseRepsRange[0]), int.Parse(exerciseRepsRange[1]) + 1);
        var randomSets = random.Next(exercise.MinSets, exercise.MaxSets + 1);
        var randomReps = random.Next(exercise.MinReps, exercise.MaxReps + 1);
        var randomDuration = random.Next(exercise.MinDurationMinutes, exercise.MaxDurationMinutes + 1);
        return new ModelInput()
        {
            UserAge = userInfo.Age,
            UserWeight = userInfo.Weight,
            UserHeight = userInfo.Height,
            UserGender = userInfo.Gender,
            UserActivityLevel = userInfo.ActivityLevel.ToString(),
            BMI = userInfo.BMI,
            BMICategory = userInfo.BMIStatus,
            StruggledPreviously = userInfo.StruggledPreviously,
            FitnessGoal = userInfo.FitnessGoal,
            DietaryPreference = string.Empty,
            AllergiesRestrictions = string.Empty,
            MealsPerDay = string.Empty,
            SnacksPerDay = string.Empty,
            ExerciseID = exercise.Id,
            ExerciseName = exercise.Name,
            ExerciseType = exercise.Type,
            ExerciseDifficulty = exercise.Difficulty.ToString(),
            ExerciseTargetMuscleGroup = exercise.TargetMuscleGroup,
            EquipmentNeeded = exercise.EquipmentNeeded ? 1.0f : 0.0f,
            RecommendedSets = randomSets,
            RecommendedReps = randomReps,
            RecommendedDurationMinutes = randomDuration,
        };
    }

    //TODO: REFACTOR FOR ABSTRACTION (DRY)
    private async Task AddExerciseToUser(Exercise exercise)
    {
        _logger.LogCritical("PRESSED!");
        var username = await _authService.ReturnUserName();
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username); // Replace with actual user ID
        if (user == null)
        {
            _logger.LogError("User is null");
            return;
        }

        var userExerciseSub = new UserExerciseSubscription
        {
            UserId = user.Id,
            ExerciseId = exercise.Id
        };
        _logger.LogInformation($"User: {user.UserName} Exercise: {exercise.Name} Type: {exercise.Type}");
        if (await dbContext.UserExerciseSubscriptions
            .AnyAsync(ues => ues.UserId == user.Id && ues.ExerciseId == exercise.Id))
        {
            _logger.LogInformation($"User: {user.UserName} already subscribed to exercise: {exercise.Name}");
            return;
        }
        dbContext.UserExerciseSubscriptions.Add(userExerciseSub);
        await dbContext.SaveChangesAsync();
    }
}
