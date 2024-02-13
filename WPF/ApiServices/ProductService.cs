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

        public async Task<ProductModel> GetById(int id)
        {
            var result = await _apiClient.GetAsync<ProductModel>($"api/products/{id}");
            return result;
        }

        public async Task Add(ProductModel model)
        {
            await _apiClient.PostAsync("api/products", model);
        }

        public async Task Update(ProductModel model)
        {
            await _apiClient.PutAsync("api/products", model);
        }

        public async Task Delete(int id)
        {
            await _apiClient.DeleteAsync($"api/products/{id}");
        }
    }
}
