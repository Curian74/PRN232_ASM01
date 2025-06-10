
using System.Net.Http;

namespace PhamQuocCuong_SE1821_A01_FE.ApiServices
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = CreateClient();
            var response = await client.GetAsync(endpoint);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = CreateClient();
            var response = await client.PostAsJsonAsync(endpoint, data);
            return await HandleResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var client = CreateClient();
            var response = await client.PutAsJsonAsync(endpoint, data);
            return await HandleResponse<T>(response);
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed: {errorContent}");
            }

            var jsonResponse = await response.Content.ReadFromJsonAsync<T>();
            return jsonResponse ?? throw new InvalidOperationException("Response content is null");
        }

    }
}
