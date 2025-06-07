using BusinessObjects;
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
    }
}
