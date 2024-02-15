using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Services;

[Route("api/wishlist")]
[ApiController]
public class WishListController : ControllerBase
{
    private readonly WishListRepository _wishListService;

    public WishListController()
    {
        _wishListService = new WishListRepository();
    }

    // GET: api/wishlist/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var wishList = await _wishListService.GetByCustomerId(id);
        if (wishList == null)
        {
            return NotFound();
        }
        return Ok(wishList);
    }

    // POST: api/wishlist
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] WishListModel wishList)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        WishListModel result = await _wishListService.Add(wishList);
        return Ok(result);
    }

    // DELETE: api/wishlist/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _wishListService.Delete(id);
        return Ok();
    }
}
