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
    }
}
