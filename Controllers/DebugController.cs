using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Controllers;

[ApiController]
[Route("debug")]
public class DebugController : ControllerBase
{
    private readonly IConfiguration configuration;

    public DebugController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpGet]
    [Route("default_connection")]
    public string GetDefaultConnectionValue()
    {
        return configuration.GetConnectionString("DefaultConnection");
    }
}