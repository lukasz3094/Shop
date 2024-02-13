using Microsoft.AspNetCore.Mvc;
using Services.Services;

[Route("api/color")]
[ApiController]
public class ColorController : ControllerBase
{
    private readonly ColorRepository _colorService;

    public ColorController()
    {
        _colorService = new ColorRepository();
    }

    // GET: api/color
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var colors = await _colorService.GetAll();
        return Ok(colors);
    }
}