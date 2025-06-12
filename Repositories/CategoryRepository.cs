using DataAccessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;

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

        public async Task<List<CategoryDto>> GetAccountsAsync(CategoryQuery query)
        {
            return await CategoryDAO.GetPaged(query);
        }
    }
}
