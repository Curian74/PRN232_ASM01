using BusinessObjects;
using BusinessObjects.Dtos;
using DataAccessObjects;
using PhamQuocCuong_SE1821_A01_BE.Dtos;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return CategoryDAO.GetCategories();
        }

        public Task<CategoryDto> GetCategoryByIdAsync(short id)
        {
            return CategoryDAO.GetByIdAsync(id);
        }

        public async Task UpdateAsync(short id, UpdateCategoryDto dto)
        {
            await CategoryDAO.Update(id, dto);
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            await CategoryDAO.Create(dto);
        }

        public async Task DeleteAsync(short id)
        {
            await CategoryDAO.Delete(id);
        }
    }
}
