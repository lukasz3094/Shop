using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;
using Services.Services;
using Services.Models;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _productService;

    public ProductsController()
    {
        _productService = new ProductRepository();
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAll();

        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _productService.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductModel product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.Add(product);
        return Ok();
    }

    // PUT: api/products/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProductModel product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productService.Update(product);
        return Ok();
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Delete(id);
        return Ok();
    }
}
