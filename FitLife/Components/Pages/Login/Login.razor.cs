using System.ComponentModel.DataAnnotations;

namespace FitLife.Components.Pages.Login;

using FitLife.Auth;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

public partial class Login
{

    [SupplyParameterFromForm(FormName = "LoginForm")]
    private UserLoginCredential Model { get; set; } = default!;

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;    

    private string errorMessage = string.Empty;

    [Inject]
    private AuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILogger<Login> Logger { get; set; } = null!;

    // For Login Validation UI
    private string validationLogin = "";
    private string validationResult = "";
    private string validationResultImg = "";
    private string hideValidationLogin = "hidden";
    private bool isLoginValid = false;

    protected override async Task OnInitializedAsync()
    {
        Model ??= new UserLoginCredential();

        if(AuthenticationStateTask is null)
        {
            Logger.LogError("AuthenticationStateTask is null, ensure the routes is wrapped in CascadingAuthenticationState");
            throw new InvalidOperationException("AuthenticationStateTask is null, ensure the routes is wrapped in CascadingAuthenticationState");
        }
        
        var authState = await AuthenticationStateTask;
        var user = authState.User;

        if (user.Identity!.IsAuthenticated)
        {
            Logger.LogInformation("User is already authenticated, redirecting to user dashboard");
            NavigationManager.NavigateTo("/user-dashboard", forceLoad: true);
        }
        else
        {
            Logger.LogInformation("User is not authenticated, showing login page");
        }

        await base.OnInitializedAsync();
    }

    private async Task HandleValidSubmit()
    {
        var result = await AuthService.SignInUser(Model!);
        if (result.Succeeded)
        {
            loginValidationSuccess();
            //NavigationManager.NavigateTo("/user-dashboard", forceLoad: true);
        }
        else
        {
            loginValidationError();
            errorMessage = "Invalid login attempt.";
            //NavigationManager.NavigateTo("/login/");
        }
        Logger.LogInformation($"Login attempt with identifier: {Model.LoginIdentifier}");
    }

    private void loginValidationSuccess()
    {
        if (!isLoginValid)
        {
            validationLogin = "overlay";
            validationResult = "Login Successfully!";
            validationResultImg = "images/login-validation/check-black.svg";
            hideValidationLogin = string.Empty;
        }
        else
        {
            validationLogin = string.Empty;
            validationResult = string.Empty;
            validationResultImg = string.Empty;
            hideValidationLogin = "hidden";
        }
        isLoginValid = !isLoginValid;
    }

    private void loginValidationError()
    {
        if (!isLoginValid)
        {
            validationLogin = "overlay";
            validationResult = "Login Failed!";
            validationResultImg = "images/login-validation/error-black.svg";
            hideValidationLogin = string.Empty;
        }
        else
        {
            validationLogin = string.Empty;
            validationResult = string.Empty;
            validationResultImg = string.Empty;
            hideValidationLogin = "hidden";
        }
        isLoginValid = !isLoginValid;
    }

}

