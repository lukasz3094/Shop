using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class BrandService
    {
        private readonly ApiClient _apiClient;

        public BrandService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<BrandModel>> GetAll()
        {
            List<BrandModel> result = await _apiClient.GetAsync<List<BrandModel>>("api/brand");
            return result;
        }
    }
}
