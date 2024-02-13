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

        public async Task<ProductModel> GetById(int id)
        {
            using (var db = new ProjectEntities())
            {
                var product = await db.Products
                    .Where(p => p.ProductID == id)
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
                    .FirstOrDefaultAsync();

                return product;
            }
        }

        public async Task Add(ProductModel product)
        {
            using (var db = new ProjectEntities())
            {
                var newProduct = new Products
                {
                    ProductName = product.ProductName,
                    CategoryID = (int)product.CategoryId,
                    ColorID = (int)product.ColorId,
                    BrandID = (int)product.BrandId,
                    Price = (decimal)product.Price,
                    StockQuantity = (int)product.Quantity,
                    Description = product.Description
                };

                db.Products.Add(newProduct);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(ProductModel product)
        {
            using (var db = new ProjectEntities())
            {
                var existingProduct = await db.Products
                    .Where(p => p.ProductID == product.Id)
                    .FirstOrDefaultAsync();

                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.CategoryID = (int)product.CategoryId;
                    existingProduct.ColorID = (int)product.ColorId;
                    existingProduct.BrandID = (int)product.BrandId;
                    existingProduct.Price = (decimal)product.Price;
                    existingProduct.StockQuantity = (int)product.Quantity;
                    existingProduct.Description = product.Description;

                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var db = new ProjectEntities())
            {
                var product = await db.Products
                    .Where(p => p.ProductID == id)
                    .FirstOrDefaultAsync();

                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }
    }
}
