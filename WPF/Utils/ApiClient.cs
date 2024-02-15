using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyWpfApp.Services
{
    public class ApiClient
    {
        private readonly string _baseApiUrl;
        private readonly HttpClient _httpClient;

        public ApiClient(string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl;
            _httpClient = new HttpClient();
        }

        // GET method
        public async Task<T> GetAsync<T>(string endpoint)
        {
            string url = $"{_baseApiUrl}/{endpoint}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        // POST method
        public async Task<T> PostAsync<T>(string endpoint, T data)
        {
            string url = $"{_baseApiUrl}/{endpoint}";
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        // PUT method
        public async Task PutAsync<T>(string endpoint, T data)
        {
            string url = $"{_baseApiUrl}/{endpoint}";
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        // DELETE method
        public async Task DeleteAsync(string endpoint)
        {
            string url = $"{_baseApiUrl}/{endpoint}";
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
