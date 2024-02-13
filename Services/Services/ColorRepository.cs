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
    public class ColorRepository
    {
        public ColorRepository() { }

        public async Task<List<ColorModel>> GetAll()
        {
            using (var db = new ProjectEntities())
            {
                var colors = await db.Colors
                    .Select(c => new ColorModel
                    {
                        Id = c.ColorID,
                        Name = c.ColorName,
                    })
                    .ToListAsync();

                return colors;
            }
        }
    }
}
