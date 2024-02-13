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
    public class CategoryRepository
    {
        public CategoryRepository() { }

        public async Task<List<CategoryModel>> GetAll()
        {
            using (var db = new ProjectEntities())
            {
                var categories = await db.Categories
                    .Select(c => new CategoryModel
                    {
                        Id = c.CategoryID,
                        Name = c.CategoryName,
                    })
                    .ToListAsync();

                return categories;
            }
        }
    }
}
