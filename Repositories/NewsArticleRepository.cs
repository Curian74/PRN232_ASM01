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

        public async Task DeleteAsync(string id)
        {
            await NewsDAO.Delete(id);
        }

        public Task<NewsDto> EditAsync(EditNewsDto dto)
        {
            return NewsDAO.Edit(dto);
        }

        public async Task<NewsDto> FindById(string id)
        {
            return await NewsDAO.FindById(id);
        }

        public Task<List<NewsDto>> GetNewsAsync(NewsQuery newsQuery)
        {
            return NewsDAO.GetNewsArticleAsync(newsQuery);
        }
    }
}
