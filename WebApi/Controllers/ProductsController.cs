using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;
using Services.Services;

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

    //// GET: api/products/5
    //[HttpGet("{id}")]
    //public IActionResult Get(int id)
    //{
    //    var product = _productService.GetProductById(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(product);
    //}

    //// POST: api/products
    //[HttpPost]
    //public IActionResult Post([FromBody] Product product)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    _productService.AddProduct(product);
    //    return CreatedAtAction("Get", new { id = product.Id }, product);
    //}

    //// PUT: api/products/5
    //[HttpPut("{id}")]
    //public IActionResult Put(int id, [FromBody] Product product)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    if (id != product.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _productService.UpdateProduct(product);
    //    return NoContent();
    //}

    //// DELETE: api/products/5
    //[HttpDelete("{id}")]
    //public IActionResult Delete(int id)
    //{
    //    var product = _productService.GetProductById(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    _productService.DeleteProduct(id);
    //    return NoContent();
    //}
}
