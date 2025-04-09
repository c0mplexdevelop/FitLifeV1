using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FitLife.Components.Pages.Homepage;

//[Authorize]
public partial class Homepage
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    [Inject]
    private ILogger<Homepage> Logger { get; set; } = null!;



    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateTask;
        var user = authState.User;
        Logger.LogWarning($"User is authenticated: {user.Identity!.IsAuthenticated}");
        Logger.LogWarning($"User name: {user.Identity.Name}");
        if (!user.Identity!.IsAuthenticated)
        {
            Navigation.NavigateTo("/");
        }
    }
}
