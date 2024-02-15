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
    public class BrandRepository
    {
        public BrandRepository() { }

        public async Task<List<BrandModel>> GetAll()
        {
            using (var db = new ProjectEntities())
            {
                List<BrandModel> brands = await db.Brands
                    .Select(b => new BrandModel
                    {
                        Id = b.BrandID,
                        Name = b.BrandName,
                    })
                    .ToListAsync();

                return brands;
            }
        }
    }
}
