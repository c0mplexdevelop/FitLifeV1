using FitLife.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FitLife.Components.Pages.UserDashboard;

public partial class UserDashboard
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

    private string userName = string.Empty;

    [Inject]
    private ILogger<UserDashboard> Logger { get; set; } = null!;

    [Inject]
    private DatabaseContext DbContext { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationStateTask is null)
        {
            throw new InvalidOperationException("AuthenticationStateTask is null, ensure the routes is wrapped in CascadingAuthenticationState");
        }

        var authState = await AuthenticationStateTask;
        var user = authState.User;
         userName = user.Identity!.Name;
        await base.OnInitializedAsync();
    }
}
