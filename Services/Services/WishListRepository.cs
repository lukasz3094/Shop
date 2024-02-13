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
    public class WishListRepository
    {
        public WishListRepository() { }

        public async Task<List<WishListModel>> GetByCustomerId(int id)
        {
            using (var db = new ProjectEntities())
            {
                var wishList = await db.Wishlist
                    .Where(w => w.CustomerID == id)
                    .Select(w => new WishListModel
                    {
                        Id = w.WishlistID,
                        CustomerId = w.CustomerID,
                        ProductId = w.ProductID,
                    })
                    .ToListAsync();

                return wishList;
            }
        }

        public async Task<WishListModel> Add(WishListModel wishList)
        {
            using (var db = new ProjectEntities())
            {
                var newWishList = new Wishlist
                {
                    CustomerID = wishList.CustomerId,
                    ProductID = wishList.ProductId,
                };

                db.Wishlist.Add(newWishList);
                await db.SaveChangesAsync();

                WishListModel result = new WishListModel
                {
                    Id = newWishList.WishlistID,
                    CustomerId = newWishList.CustomerID,
                    ProductId = newWishList.ProductID,
                };

                return result;
            }
        }

        public async Task Delete(int id)
        {
            using (var db = new ProjectEntities())
            {
                var wishList = await db.Wishlist
                    .Where(w => w.WishlistID == id)
                    .FirstOrDefaultAsync();

                if (wishList != null)
                {
                    db.Wishlist.Remove(wishList);
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
