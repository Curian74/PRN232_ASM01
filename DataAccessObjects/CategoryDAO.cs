using BusinessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;
using Microsoft.EntityFrameworkCore;

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

        public static async Task<CategoryDto> GetByIdAsync(short id)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                    if (category == null)
                    {
                        return null;
                    }

                    var dtoEntity = new CategoryDto
                    {
                        CategoryId = category.CategoryId,
                        ParentCategoryId = category.ParentCategoryId,
                        IsActive = category.IsActive,
                        CategoryDesciption = category.CategoryDesciption,
                        CategoryName = category.CategoryName,
                    };

                    return dtoEntity;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task Update(short id, UpdateCategoryDto dto)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var category = new Category
                    {
                        CategoryId = id,
                        CategoryName = dto.CategoryName,
                        CategoryDesciption = dto.CategoryDesciption,
                        IsActive = dto.IsActive,
                        ParentCategoryId = dto.ParentCategoryId,
                    };

                    context.Entry<Category>(category).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task Create(CreateCategoryDto dto)
        {
            try
            {
                using var context = new FunewsManagementContext();

                var category = new Category
                {
                    CategoryName = dto.CategoryName,
                    CategoryDesciption = dto.CategoryDesciption,
                    IsActive = dto.IsActive,
                    ParentCategoryId = dto.ParentCategoryId,
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }

            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task Delete(short id)
        {
            try
            {
                using var context = new FunewsManagementContext();
                var category = context.Categories
                    .Include(c => c.NewsArticles)
                    .FirstOrDefault(c => c.CategoryId == id)
                    ?? throw new KeyNotFoundException("Category not found.");

                if (category.NewsArticles.Count > 0)
                {
                    throw new InvalidOperationException("Cannot delete a category with articles posted.");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }

            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<List<CategoryDto>> GetPaged(CategoryQuery query)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var accounts = context.Categories.AsQueryable();

                    var dtoEntities = await accounts.Select(x => new CategoryDto
                    {
                       CategoryDesciption = x.CategoryDesciption,
                       CategoryId = x.CategoryId,
                       CategoryName = x.CategoryName,
                       IsActive = x.IsActive,
                       ParentCategoryId = x.ParentCategoryId
                    })
                        .ToListAsync();

                    return dtoEntities;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
