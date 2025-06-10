using DataAccessObjects.Dtos;

namespace Repositories
{
    public interface INewsArticleRepository
    {
        Task<List<NewsDto>> GetNewsAsync();
    }
}
