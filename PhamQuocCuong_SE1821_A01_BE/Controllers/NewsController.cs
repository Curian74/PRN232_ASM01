using DataAccessObjects;
using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            var skip = (newsQuery.PageIndex - 1) * newsQuery.PageSize;

            var pagedData = newsArticles.Skip(skip).Take(newsQuery.PageSize);

            var data = new PagedResult<NewsDto>(pagedData,
                newsQuery.PageIndex, newsQuery.PageSize, newsArticles.Count);

            return Ok(data);
        }
    }
}
