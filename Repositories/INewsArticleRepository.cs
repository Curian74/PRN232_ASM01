using DataAccessObjects;
using DataAccessObjects.Dtos;

namespace Repositories
{
    public interface INewsArticleRepository
    {
        Task<List<NewsDto>> GetNewsAsync(NewsQuery newsQuery);
        Task<List<NewsDto>> GetReportAsync(DateTime? startDate, DateTime? endDate);
        Task<NewsDto> CreateAsync(CreateNewsDto dto);
        Task<NewsDto> FindById(string id);
        Task<NewsDto> EditAsync(EditNewsDto dto);
        Task DeleteAsync(string id);
    }
}
