using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class CartService
    {
        private readonly ApiClient _apiClient;

        public CartService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<CartModel>> GetById(int id)
        {
            List<CartModel> result = await _apiClient.GetAsync<List<CartModel>>($"api/cart/{id}");
            return result;
        }

        public async Task<CartModel> Add(CartModel model)
        {
            return await _apiClient.PostAsync($"api/cart", model);
        }

        public async Task Update(CartModel model)
        {
            await _apiClient.PutAsync($"api/cart", model);
        }

        public async Task Delete(int id)
        {
            await _apiClient.DeleteAsync($"api/cart/{id}");
        }
    }
}
