﻿using System.ComponentModel.DataAnnotations;

namespace FitLife.Components.Pages.Login;

public partial class Login
{
    private UserCredential Model = new();
}

public class UserCredential
{
    public string LoginIdentifier { get; set; } = null!;

    public string Password { get; set; } = null!;
}
