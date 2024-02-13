using Microsoft.AspNetCore.Mvc;
using Services;
using Entities;
using Services.Services;
using Services.Models;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly CartRepository _cartService;

    public CartController()
    {
        _cartService = new CartRepository();
    }

    // GET: api/cart/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var cart = await _cartService.GetByCustomerId(id);
        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    // POST: api/cart
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CartModel cart)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        CartModel result = await _cartService.Add(cart);
        return Ok(result);
    }

    // PUT: api/cart/5
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CartModel cart)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cartService.Update(cart);
        return Ok();
    }

    // DELETE: api/cart/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cartService.Delete(id);
        return Ok();
    }
}
