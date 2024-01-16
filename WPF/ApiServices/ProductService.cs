using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class ProductService
    {
        private readonly ApiClient _apiClient;

        public ProductService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<ProductModel>> GetAll()
        {
            var result = await _apiClient.GetAsync<List<ProductModel>>("api/products");
            return result;
        }
    }
}
