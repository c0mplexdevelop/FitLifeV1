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
            NavigationManager.NavigateTo("/homepage/");
        }
        else
        {
            errorMessage = "Invalid login attempt.";
            NavigationManager.NavigateTo("/login/");
        }
    }

}

