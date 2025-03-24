using FitLife.Models.SignUp;

namespace FitLife.Models.State;

public class UserSignUpState
{
    public UserSignUpCredential UserSignUpCredential { get; set; } = new();
    public UserSignUpInformation UserSignUpInformation { get; set; } = new();
}
