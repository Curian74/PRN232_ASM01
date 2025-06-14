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

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _apiService
                .GetAsync<List<CategoryDto>>($"Categories");

            return response;
        }

        public async Task<PagedResult<CategoryDto>> GetCategoriesAsync(int? pageIndex, int? pageSize, string? searchTerm)
        {
            var response = await _apiService
                .GetAsync<PagedResult<CategoryDto>>($"Categories/Paged?" +
                $"PageIndex={pageIndex}&PageSize={pageSize}&searchTerm={searchTerm}");

            return response;
        }

        public async Task<CreateCategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var response = await _apiService
                .PostAsync<CreateCategoryDto>($"Categories", dto);

            return response;
        }

        public async Task<CategoryDto> GetByIdAsync(short id)
        {
            var response = await _apiService
                .GetAsync<CategoryDto>($"Categories/{id}");

            return response;
        }

        public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto)
        {
            var response = await _apiService
                .PutAsync<CategoryDto>($"Categories/Edit", dto);

            return response;
        }

        public async Task<bool> Delete(short id)
        {
            var response = await _apiService
                .DeleteAsync($"Categories/{id}");

            return response;
        }
    }
}
