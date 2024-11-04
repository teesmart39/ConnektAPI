using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Routes;
using ConnektAPI_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnektAPI.Controllers;

public class AuthController : Controller
{
    
    private readonly IAuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost(EndpointRoutes.SignUp)]
    public async Task<ActionResult> SignUp([FromBody] SignUpRequestModel signUpRequestModel)
    {
        var operation = await authService.SignUp(signUpRequestModel);

        if (!operation.IsSuccess)
        {
            return BadRequest(operation);
        }
        return Ok(operation);
    }
}