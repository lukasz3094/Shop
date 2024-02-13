using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class WishListService
    {
        private readonly ApiClient _apiClient;

        public WishListService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<WishListModel>> GetById(int id)
        {
            List<WishListModel> result = await _apiClient.GetAsync<List<WishListModel>>($"api/wishlist/{id}");
            return result;
        }

        public async Task<WishListModel> Add(WishListModel model)
        {
            WishListModel result = await _apiClient.PostAsync($"api/wishlist", model);
            return result;
        }

        public async Task Delete(int id)
        {
            await _apiClient.DeleteAsync($"api/wishlist/{id}");
        }
    }
}
