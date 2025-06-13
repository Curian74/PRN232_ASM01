using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(short id);
        Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task DeleteAsync(short id);
        Task<List<CategoryDto>> GetAccountsAsync(CategoryQuery query);
    }
}
