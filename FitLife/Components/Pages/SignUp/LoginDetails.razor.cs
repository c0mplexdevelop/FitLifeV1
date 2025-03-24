using FitLife.Models.State;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.SignUp;

public partial class LoginDetails
{
    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    [Inject]
    private ILogger<LoginDetails> Logger { get; set; } = null!;

    [Inject]
    private UserSignUpState State { get; set; } = null!;

    private void HandleValidSubmit()
    {
        Navigation.NavigateTo("/signup/personal-details");
        Logger.LogInformation("Valid form submission");
        Logger.LogInformation(State.UserSignUpCredential.Email);
    }

    private void HandleInvalidSubmit()
    {
        Logger.LogError("Invalid form submission");
        Logger.LogError(State.UserSignUpCredential.Email);
    }
}
