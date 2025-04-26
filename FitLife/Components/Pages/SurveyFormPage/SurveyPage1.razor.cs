using FitLife.Models.Survey;
using FitLife.Services;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.SurveyFormPage;

public partial class SurveyPage1
{

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private SurveyService _surveyService { get; set; } = default!;

    private SurveyModel? _surveyModel;

    protected override void OnInitialized()
    {
        _surveyModel = _surveyService.SurveyModel;
    }

    private void OnValidSubmit()
    {
        // Save the survey model to the database or perform any other action
        // For example, you can navigate to another page after submission
        NavigationManager.NavigateTo("/survey-page2");
    }
}
