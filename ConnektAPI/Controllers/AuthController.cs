using ConnektAPI_Core.ApiModels.Auth.Login;
using ConnektAPI_Core.ApiModels.Auth.Register;
using ConnektAPI_Core.Routes;
using ConnektAPI_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnektAPI.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost(EndpointRoutes.SignUp)]
    public async Task<ActionResult> SignUp([FromBody] SignUpRequestModel signUpRequestModel)
    {
        var operation = await authService.SignUp(signUpRequestModel);

        if (!operation.IsSuccess) return BadRequest(operation);
        return Ok(operation);
    }

    [HttpPost(EndpointRoutes.Login)]
    public async Task<ActionResult> Login([FromBody] LoginRequestModel loginRequestModel)
    {
        var operation = await authService.Login(loginRequestModel);

        if (!operation.IsSuccess) return BadRequest(operation);
        return Ok(operation);
    }
}