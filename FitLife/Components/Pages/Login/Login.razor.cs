using System.ComponentModel.DataAnnotations;

namespace FitLife.Components.Pages.Login;

using FitLife.Auth;
using FitLife.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

public partial class Login
{

    [SupplyParameterFromForm]
    private UserLoginCredential? Model { get; set; }

    private string errorMessage = string.Empty;

    [Inject]
    private AuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ILogger<Login> Logger { get; set; } = null!;

    private bool IsPasswordVisible { get; set; } = false;
    private string PasswordVisibilityState { get; set; } = "password";
    private string EyeVisibilityState { get; set; } = "fa-eye-slash";

    private void ChangePasswordVisibility(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
    {
        PasswordVisibilityState = IsPasswordVisible ? "text" : "password";
        EyeVisibilityState = IsPasswordVisible ? "fa-eye" : "fa-eye-slash";
        IsPasswordVisible = !IsPasswordVisible;
        Logger.LogInformation($"Password visibility changed to: {PasswordVisibilityState}");
    }

    protected override Task OnInitializedAsync()
    {
        Model ??= new UserLoginCredential();
        return base.OnInitializedAsync();
    }

    private async Task HandleValidSubmit()
    {
        var result = await AuthService.SignInUser(Model!);
        if (result.Succeeded)
        {
            NavigationManager.NavigateTo("/user-dashboard");
        }
        else
        {
            errorMessage = "Invalid login attempt.";
            NavigationManager.NavigateTo("/login/");
        }
    }

}

