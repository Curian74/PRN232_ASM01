using BusinessObjects;
using DataAccessObjects.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class NewsDAO
    {
        public static async Task<List<NewsDto>> GetNewsArticleAsync(NewsQuery newsQuery)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var newsQueryable = context.NewsArticles.AsQueryable();

                    if (newsQuery.IsActive.HasValue)
                    {
                        newsQueryable = newsQueryable.Where(n => n.NewsStatus == newsQuery.IsActive);
                    }

                    var skip = (newsQuery.PageIndex - 1) * newsQuery.PageSize;

                    var pagedData = newsQueryable.Skip(skip).Take(newsQuery.PageSize);

                    var dtoEntities = await pagedData.Select(n => new NewsDto
                    {
                        NewsArticleId = n.NewsArticleId,
                        CategoryId = n.CategoryId,
                        CreatedById = n.CreatedById,
                        CreatedDate = n.CreatedDate,
                        Headline = n.Headline,
                        ModifiedDate = n.ModifiedDate,
                        NewsContent = n.NewsContent,
                        NewsSource = n.NewsSource,
                        NewsStatus = n.NewsStatus,
                        NewsTitle = n.NewsTitle,
                        UpdatedById = n.UpdatedById,
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
