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

        var operation = await userService.GetUsers();
        if (!operation.IsSuccess)
            return Problem(operation?.ErrorMessage, statusCode: operation?.StatusCode,
                title: operation?.ErrorTitle);

        return Ok(operation?.Result);
    }
    
    [HttpGet(EndpointRoutes.GetUser)]
    public async Task<ActionResult> GetUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        
        var operation = await userService.GetUserById(userId);
        if (!operation.IsSuccess)
            return Problem(operation?.ErrorMessage, statusCode: operation?.StatusCode,
                title: operation?.ErrorTitle);

        return Ok(operation?.Result);
    }
}