using Microsoft.AspNetCore.Mvc;
using Services.Services;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository _categoryService;

    public CategoryController()
    {
        _categoryService = new CategoryRepository();
    }

    // GET: api/category
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _categoryService.GetAll();
        return Ok(categories);
    }
}