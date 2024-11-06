using System.Net;
using System.Security.Claims;
using ConnektAPI_Core.Routes;
using ConnektAPI_Core.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnektAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet(EndpointRoutes.GetUsers)]
    public async Task<ActionResult> GetUsers()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var fetchCustomer = await userService.GetUserById(userId);
        if (fetchCustomer == null)
            return Problem(fetchCustomer?.ErrorMessage, statusCode: (int)HttpStatusCode.NotFound,
                title: fetchCustomer?.ErrorTitle);

        var operation = userService.GetUsers();
        if (!operation.Result.IsSuccess)
            return Problem(operation?.Result.ErrorMessage, statusCode: operation?.Result.StatusCode,
                title: operation?.Result.ErrorTitle);

        return Ok(operation?.Result);
    }
}