using DataAccessObjects.Dtos;
using DataAccessObjects;
using PhamQuocCuong_SE1821_A01_FE.ApiServices;

namespace PhamQuocCuong_SE1821_A01_FE.Services
{
    public class CategoryService
    {
        private readonly IApiService _apiService;

        public CategoryService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int? pageIndex, int? pageSize)
        {
            var response = await _apiService
                .GetAsync<PagedResult<CategoryDto>>($"Categories/Paged?PageIndex={pageIndex}&PageSize={pageSize}");

            return response;
        }

        public async Task<CreateCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var response = await _apiService
                .PostAsync<CreateCategoryDto>($"Categories", dto);

            return response;
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
