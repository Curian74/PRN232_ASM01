using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using PhamQuocCuong_SE1821_A01_BE.Dtos;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        public static async Task<List<CategoryDto>> GetCategories()
        {
            var categories = new List<Category>();

            try
            {
                using (var context = new FunewsManagementContext())
                {
                    categories = await context.Categories.ToListAsync();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return categories.Select(x => new CategoryDto
            {
                CategoryDesciption = x.CategoryDesciption,
                CategoryName = x.CategoryName,
                CategoryId = x.CategoryId,
                IsActive = x.IsActive,
                ParentCategoryId = x.ParentCategoryId,
            }).ToList();
        }
    }
}
