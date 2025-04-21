namespace FitLife.Components.Pages.SurveyFormPage
{
    public partial class SurveyPage1
    {
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Task.Delay(TimeSpan.FromSeconds(3));

            isLoading = false;
        }
    }
}
