using Microsoft.AspNetCore.Authorization;

namespace FitLife.Components.Pages.SurveyFormPage
{
    [Authorize]
    public partial class StartSurvey
    {
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Task.Delay(TimeSpan.FromSeconds(2.5));

            isLoading = false;
        }
    }
}
