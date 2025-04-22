namespace FitLife.Components.Pages.SurveyFormPage
{
    public partial class GeneratedGoalAI
    {
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Task.Delay(TimeSpan.FromSeconds(4));

            isLoading = false;
        }
    }
}
