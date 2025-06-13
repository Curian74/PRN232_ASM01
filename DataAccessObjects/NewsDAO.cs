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

                    var dtoEntities = await newsQueryable.Select(n => new NewsDto
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
                        AuthorName = n.CreatedBy.AccountName,
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

        public static async Task<NewsDto> Create(CreateNewsDto dto)
        {
            using var context = new FunewsManagementContext();

            var check = await context.NewsArticles
                .FirstOrDefaultAsync(n => n.NewsArticleId == dto.NewsArticleId)
                != null;

            if (check)
            {
                throw new InvalidOperationException("News article with the id already existed.");
            }

            var newNews = new NewsArticle
            {
                NewsArticleId = dto.NewsArticleId,
                CategoryId = dto.CategoryId,
                CreatedById = dto.CreatedById,
                CreatedDate = DateTime.Now,
                Headline = dto.Headline,
                ModifiedDate = null,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                NewsStatus = dto.NewsStatus,
                NewsTitle = dto.NewsTitle,
            };

            await context.NewsArticles.AddAsync(newNews);
            await context.SaveChangesAsync();

            return new NewsDto
            {
                NewsTitle = newNews.NewsTitle,
                NewsContent = newNews.NewsContent,
                NewsSource = newNews.NewsSource,
                NewsStatus = newNews.NewsStatus,
                AuthorName = "",
                CreatedById = newNews.CreatedById,
                CreatedDate = DateTime.Now,
                CategoryId = newNews.CategoryId,
                ModifiedDate = DateTime.Now,
                Headline = newNews.Headline,
                NewsArticleId = newNews.NewsArticleId,
                UpdatedById = null,
            };
        }
    }
}
