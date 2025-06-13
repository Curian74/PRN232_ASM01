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

        public static async Task<NewsDto> FindById(string id)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var news = await context.NewsArticles
                        .FirstOrDefaultAsync(a => a.NewsArticleId == id);

                    if (news == null)
                    {
                        return null;
                    }

                    return new NewsDto
                    {
                        NewsArticleId = news.NewsArticleId,
                        CategoryId = news.CategoryId,
                        CreatedById = news.CreatedById,
                        CreatedDate = news.CreatedDate,
                        Headline = news.Headline,
                        ModifiedDate = news.ModifiedDate,
                        NewsContent = news.NewsContent,
                        NewsSource = news.NewsSource,
                        NewsStatus = news.NewsStatus,
                        NewsTitle = news.NewsTitle,
                        UpdatedById = news.UpdatedById
                    };
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<NewsDto> Edit(EditNewsDto dto)
        {
            using (var context = new FunewsManagementContext())
            {
                var news = await context.NewsArticles.FindAsync(dto.NewsArticleId);

                if (news == null)
                {
                    throw new KeyNotFoundException("News not found");
                }

                news.NewsTitle = dto.NewsTitle;
                news.Headline = dto.Headline;
                news.NewsContent = dto.NewsContent;
                news.NewsSource = dto.NewsSource;
                news.CategoryId = dto.CategoryId;
                news.NewsStatus = dto.NewsStatus;
                news.ModifiedDate = DateTime.Now;
                news.UpdatedById = dto.CreatedById;

                context.NewsArticles.Update(news);
                await context.SaveChangesAsync();

                return new NewsDto
                {
                    NewsArticleId = news.NewsArticleId,
                    NewsTitle = news.NewsTitle,
                    Headline = news.Headline,
                    NewsContent = news.NewsContent,
                    NewsSource = news.NewsSource,
                    CategoryId = news.CategoryId,
                    NewsStatus = news.NewsStatus,
                    CreatedById = news.CreatedById,
                    CreatedDate = news.CreatedDate,
                    ModifiedDate = news.ModifiedDate,
                    UpdatedById = news.UpdatedById
                };
            }
        }

        public static async Task Delete(string id)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var news = await context.NewsArticles
                        .Include(x => x.Tags)
                        .FirstOrDefaultAsync(a => a.NewsArticleId == id);

                    if (news == null)
                    {
                        throw new Exception("Cannot find the news.");
                    }

                    news.Tags.Clear();
                    context.NewsArticles.Remove(news);
                    context.SaveChanges();
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
