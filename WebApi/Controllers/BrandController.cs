using Microsoft.AspNetCore.Mvc;
using Services.Services;

[Route("api/brand")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly BrandRepository _brandService;

    public BrandController()
    {
        _brandService = new BrandRepository();
    }

    // GET: api/brand
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var brands = await _brandService.GetAll();
        return Ok(brands);
    }
}
