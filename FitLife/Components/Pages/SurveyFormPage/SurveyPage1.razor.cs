using FitLife.Auth;
using FitLife.Data;
using FitLife.Models.Survey;
using FitLife.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FitLife.Components.Pages.SurveyFormPage;

public partial class SurveyPage1
{

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private SurveyService _surveyService { get; set; } = default!;

    [Inject]
    private DatabaseContext dbContext { get; set; } = default!;

    [Inject]
    private AuthService _authService { get; set;} = default!;
    private SurveyModel? _surveyModel;

    [Inject]
    private ILogger<SurveyPage1> _logger { get; set; } = default!;

    protected override void OnInitialized()
    {
        _surveyService.ResetSurveyModel();
        _surveyModel = _surveyService.SurveyModel;
    }

    private async Task OnValidSubmit()
    {
        // Save the survey model to the database or perform any other action
        // For example, you can navigate to another page after submission
        var username = await _authService.ReturnUserName();
        var user = await dbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username);
        _surveyModel.Gender = user.Sex.ToString();
        _logger.LogWarning($"Var surveymodel: {_surveyModel.Age}");
        _logger.LogWarning($"Service surveyModel: {_surveyService.SurveyModel.Age}");
        NavigationManager.NavigateTo("/survey-page2");
    }
}
