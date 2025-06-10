using DataAccessObjects;
using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace PhamQuocCuong_SE1821_A01_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsController(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Paged([FromQuery] NewsQuery newsQuery)
        {
            var newsArticles = await _newsArticleRepository.GetNewsAsync(newsQuery);

            var data = new PagedResult<NewsDto>(newsArticles,
                newsQuery.PageIndex, newsQuery.PageSize, newsArticles.Count);

            return Ok(data);
        }
    }
}
