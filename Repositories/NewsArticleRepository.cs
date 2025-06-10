using DataAccessObjects;
using DataAccessObjects.Dtos;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public Task<List<NewsDto>> GetNewsAsync()
        {
            return NewsDAO.GetNewsArticleAsync();
        }
    }
}
