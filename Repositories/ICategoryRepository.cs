using PhamQuocCuong_SE1821_A01_BE.Dtos;

namespace Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategoriesAsync();
    }
}
