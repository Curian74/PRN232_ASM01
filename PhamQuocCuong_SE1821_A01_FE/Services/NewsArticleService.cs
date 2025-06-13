using DataAccessObjects;
using DataAccessObjects.Dtos;
using PhamQuocCuong_SE1821_A01_FE.ApiServices;

namespace PhamQuocCuong_SE1821_A01_FE.Services
{
    public class NewsArticleService
    {
        private readonly IApiService _apiService;

        public NewsArticleService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<PagedResult<NewsDto>> GetNews(int? pageIndex, int? pageSize, bool isActive)
        {
            var response = await _apiService
                .GetAsync<PagedResult<NewsDto>>($"News?PageIndex={pageIndex}&PageSize={pageSize}&isActive={isActive}");

            return response;
        }

        public async Task CreateAsync(CreateNewsDto dto)
        {
            var response = await _apiService
                .PostAsync<NewsDto>($"News", dto);
        }

        public async Task<NewsDto> GetByIdAsync(string id)
        {
            var response = await _apiService
                .GetAsync<NewsDto>($"News/{id}");

            return response;
        }

        public async Task<NewsDto> UpdateAsync(EditNewsDto dto)
        {
            var response = await _apiService
                .PutAsync<NewsDto>($"News/Edit", dto);

            return response;
        }

        public async Task<bool> Delete(string id)
        {
            var response = await _apiService
                .DeleteAsync($"News/{id}");

            return response;
        }
    }
}
