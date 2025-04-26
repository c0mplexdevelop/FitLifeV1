using FitLife.Data;
using FitLife.Models.Exercises;
using FitLife.Models.Survey;
using FitLife.Models.User;
using FitLife.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Components.Pages.SurveyFormPage;

public partial class SurveyPage2
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private SurveyService _surveyService { get; set; } = default!;

    private SurveyModel? _surveyModel = new SurveyModel();

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    [Inject]
    private ILogger<SurveyPage2> _logger { get; set; } = default!;

    private List<Exercise> exercises = [];
    protected override async Task OnInitializedAsync()
    {
        // Fetch the list of exercises from the database
        exercises = await dbContext.Exercises.AsNoTracking().ToListAsync();
        _surveyModel = _surveyService.SurveyModel;
        // Check if the survey model is null

        _logger.LogInformation($"SurveyModel: {_surveyModel is null}");
        await base.OnInitializedAsync();

    }

    private void OnValidSubmit()
    {
        // Save the survey model to the database or perform any other action
        // For example, you can navigate to another page after submission
        NavigationManager.NavigateTo("/ai-generatedgoal");
    }

    private string viewExerciseHistory = "hidden";
    private string viewAddedExerciseHistory = "hidden";
    private bool isOverlayOpen = false;
    private bool isOverlayOpen2 = false;

    private void selectExerciseHistories(MouseEventArgs e)
    {
        if (!isOverlayOpen)
        {
            viewExerciseHistory = string.Empty;
        }
        else
        {
            viewExerciseHistory = "hidden";
        }
        isOverlayOpen = !isOverlayOpen;

    }

    private void addedExerciseHistories(MouseEventArgs e)
    {
        if (!isOverlayOpen2)
        {
            viewAddedExerciseHistory = string.Empty;
        }
        else
        {
            viewAddedExerciseHistory = "hidden";
        }
        isOverlayOpen2 = !isOverlayOpen2;

    }

    private void closeSelectExerciseHistories(MouseEventArgs e)
    {
        if (isOverlayOpen)
        {
            viewExerciseHistory = "hidden";
        }
        else
        {
            viewExerciseHistory = string.Empty;
        }
        isOverlayOpen = !isOverlayOpen;
    }

    private void closeAddedExerciseHistories(MouseEventArgs e)
    {
        if (isOverlayOpen2)
        {
            viewAddedExerciseHistory = "hidden";
        }
        else
        {
            viewAddedExerciseHistory = string.Empty;
        }
        isOverlayOpen2 = !isOverlayOpen2;
    }

    private void confirmedAddedExerciseHistories(MouseEventArgs e)
    {
        if (isOverlayOpen || isOverlayOpen2)
        {
            viewAddedExerciseHistory = "hidden";
            viewExerciseHistory = "hidden";
        }
        else
        {
            viewAddedExerciseHistory = string.Empty;
            viewExerciseHistory = string.Empty;
        }
        isOverlayOpen = !isOverlayOpen;
        isOverlayOpen2 = !isOverlayOpen2;
    }

    private void AssignExerciseToUser(Exercise exercise)
    {
        if(_surveyModel!.Exercises.Contains(exercise))
        {
            _logger.LogWarning($"Exercise {exercise.Name} already exists in the list.");
        }

        else
        {
            _surveyModel.Exercises.Add(exercise);
            _logger.LogInformation($"Exercise {exercise.Name} added to the list.");
        }
    }
}
