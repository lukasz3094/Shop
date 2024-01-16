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
    public class ProductRepository
    {
        public ProductRepository()
        {
        }

        public async Task<List<ProductModel>> GetAll()
        {
            using (var db = new ProjectEntities())
            {
                var products = await db.Products
                    .Select(p => new ProductModel
                    {
                        Id = p.ProductID,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryID,
                        CategoryName = p.Categories.CategoryName,
                        ColorId = p.ColorID,
                        ColorName = p.Colors.ColorName,
                        BrandId = p.BrandID,
                        BrandName = p.Brands.BrandName,
                        Price = p.Price,
                        Quantity = p.StockQuantity,
                        Description = p.Description
                    })
                    .ToListAsync();

                return products;
            }
        }

        //public async Task<Product> GetByIdAsync(int id)
        //{
        //    return await _dbContext.Products.FindAsync(id);
        //}

        //public async Task<Product> CreateAsync(Product product)
        //{
        //    _dbContext.Products.Add(product);
        //    await _dbContext.SaveChangesAsync();
        //    return product;
        //}

        //public async Task<Product> UpdateAsync(Product product)
        //{
        //    _dbContext.Entry(product).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();
        //    return product;
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var product = await _dbContext.Products.FindAsync(id);
        //    _dbContext.Products.Remove(product);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}
