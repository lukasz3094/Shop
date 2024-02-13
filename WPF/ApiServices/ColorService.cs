using MyWpfApp.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ApiServices
{
    public class ColorService
    {
        private readonly ApiClient _apiClient;

        public ColorService()
        {
            _apiClient = App.ApiClient;
        }

        public async Task<List<ColorModel>> GetAll()
        {
            List<ColorModel> result = await _apiClient.GetAsync<List<ColorModel>>("api/color");
            return result;
        }
    }
}
