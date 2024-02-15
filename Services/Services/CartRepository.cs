using Entities.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CartRepository
    {
        public CartRepository() { }

        public async Task<List<CartModel>> GetByCustomerId(int id)
        {
            using (var db = new ProjectEntities())
            {
                var cart = await db.Cart
                    .Where(c => c.CustomerID == id)
                    .Select(c => new CartModel
                    {
                        Id = c.CartID,
                        CustomerId = c.CustomerID,
                        ProductId = c.ProductID,
                        Quantity = c.Quantity,
                    })
                    .ToListAsync();

                return cart;
            }
        }

        public async Task<CartModel> Add(CartModel cart)
        {
            using (var db = new ProjectEntities())
            {
                var newCart = new Cart
                {
                    CustomerID = cart.CustomerId,
                    ProductID = cart.ProductId,
                    Quantity = cart.Quantity,
                };

                db.Cart.Add(newCart);
                db.Products.FirstOrDefaultAsync(p => p.ProductID == cart.ProductId).Result.StockQuantity -= 1;

                await db.SaveChangesAsync();

                CartModel result = new CartModel
                {
                    Id = newCart.CartID,
                    CustomerId = newCart.CustomerID,
                    ProductId = newCart.ProductID,
                    Quantity = newCart.Quantity,
                };

                return result;
            }
        }

        public async Task Update(CartModel cart)
        {
            using (var db = new ProjectEntities())
            {
                var existingCart = await db.Cart
                    .Where(c => c.CartID == cart.Id)
                    .FirstOrDefaultAsync();

                if (existingCart != null)
                {
                    existingCart.CustomerID = cart.CustomerId;
                    existingCart.ProductID = cart.ProductId;
                    existingCart.Quantity = cart.Quantity;

                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var db = new ProjectEntities())
            {
                var cart = await db.Cart
                    .Where(c => c.CartID == id)
                    .FirstOrDefaultAsync();

                db.Products.FirstOrDefaultAsync(p => p.ProductID == cart.ProductID).Result.StockQuantity += 1;

                if (cart != null)
                {
                    db.Cart.Remove(cart);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
