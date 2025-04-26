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
    private bool isOverlayOpen = false;

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
}
