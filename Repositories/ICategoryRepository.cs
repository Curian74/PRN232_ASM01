using BusinessObjects.Dtos;
using PhamQuocCuong_SE1821_A01_BE.Dtos;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(short id);
        Task UpdateAsync(short id, UpdateCategoryDto dto);
        Task CreateAsync(CreateCategoryDto dto);
    }
}
