using System.ComponentModel.DataAnnotations;

namespace ConnektAPI_Core.ApiModels.Auth.Register;

public class SignUpRequestModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [Compare("Password")] public string ConfirmedPassword { get; set; }
}