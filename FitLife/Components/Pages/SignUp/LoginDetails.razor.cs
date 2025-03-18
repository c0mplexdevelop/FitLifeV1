using FitLife.Models.User;
using Microsoft.AspNetCore.Components;

namespace FitLife.Components.Pages.SignUp;

public partial class LoginDetails
{
    [SupplyParameterFromForm]
    private UserCredential? Model { get; set; } = new UserCredential();
}
