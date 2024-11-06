using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnektAPI.Controllers;

[ApiController]
//[Authorize]
public class TestController : Controller
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet("api/test")]
    public ActionResult Test()
    {
        return Ok();
    }
}