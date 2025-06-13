using DataAccessObjects;
using DataAccessObjects.Dtos;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public Task<NewsDto> CreateAsync(CreateNewsDto dto)
        {
            return NewsDAO.Create(dto);
        }

        public Task<List<NewsDto>> GetNewsAsync(NewsQuery newsQuery)
        {
            return NewsDAO.GetNewsArticleAsync(newsQuery);
        }
    }
}
