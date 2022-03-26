using BlogRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogRest.Controllers;

[ApiController]
[Route("settings")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsService service;

    public SettingsController(ISettingsService service)
    {
        this.service = service;
    }

    [HttpGet("/{name}")]
    public IActionResult GetSettingByName(string name)
    {
        string value = service.GetSettingByName(name);

        if (string.IsNullOrEmpty(value))
        {
            return NotFound();
        }
        
        return Ok(value);
    }
}