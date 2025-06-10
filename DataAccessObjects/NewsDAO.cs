using BusinessObjects;
using DataAccessObjects.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class NewsDAO
    {
        public static async Task<List<NewsDto>> GetNewsArticleAsync()
        {
            var newsArticles = new List<NewsArticle>();

            try
            {
                using (var context = new FunewsManagementContext())
                {
                    newsArticles = await context.NewsArticles.ToListAsync();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return newsArticles.Select(n => new NewsDto
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
            }).ToList();
        }
    }
}
