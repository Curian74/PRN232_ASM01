using DataAccessObjects;
using DataAccessObjects.Dtos;

namespace Repositories
{
    public interface INewsArticleRepository
    {
        Task<List<NewsDto>> GetNewsAsync(NewsQuery newsQuery);
        Task<NewsDto> CreateAsync(CreateNewsDto dto);
    }
}
