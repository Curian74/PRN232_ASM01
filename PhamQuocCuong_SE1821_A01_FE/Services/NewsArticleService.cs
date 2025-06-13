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

        public async Task<PagedResult<NewsDto>> GetNews(int? pageIndex, int? pageSize)
        {
            var response = await _apiService
                .GetAsync<PagedResult<NewsDto>>($"News?PageIndex={pageIndex}&PageSize={pageSize}");

            return response;
        }

        public async Task CreateAsync(CreateNewsDto dto)
        {
            var response = await _apiService
                .PostAsync<NewsDto>($"News", dto);
        }

        public async Task<SystemAccountDto> GetByIdAsync(short id)
        {
            var response = await _apiService
                .GetAsync<SystemAccountDto>($"SystemAccounts/{id}");

            return response;
        }

        public async Task<SystemAccountDto> UpdateAsync(EditAccountDto dto)
        {
            var response = await _apiService
                .PutAsync<SystemAccountDto>($"SystemAccounts/Edit", dto);

            return response;
        }

        public async Task<bool> Delete(short id)
        {
            var response = await _apiService
                .DeleteAsync($"SystemAccounts/{id}");

            return response;
        }
    }
}
