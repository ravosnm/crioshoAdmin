using Criosho.Admin.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Criosho.Admin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get()
    {
        return Ok(ApiResponse<object>.Ok(new { status = "healthy", utcNow = DateTime.UtcNow }, "API operativa."));
    }
}
