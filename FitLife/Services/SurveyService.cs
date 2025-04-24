using FitLife.Models.Survey;

namespace FitLife.Services;

public class SurveyService
{
    public SurveyModel SurveyModel { get; private set; } = new SurveyModel();

    public void ResetSurveyModel()
    {
        SurveyModel = new SurveyModel();
    }
}
