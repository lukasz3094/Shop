using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class CategoryService
    {
        private readonly ApiClient _apiClient;

        public CategoryService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<CategoryModel>> GetAll()
        {
            List<CategoryModel> result = await _apiClient.GetAsync<List<CategoryModel>>("api/category");
            return result;
        }
    }
}
