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

            var skip = (newsQuery.PageIndex - 1) * newsQuery.PageSize;

            var pagedData = newsArticles.Skip(skip).Take(newsQuery.PageSize);

            var data = new PagedResult<NewsDto>(pagedData,
                newsQuery.PageIndex, newsQuery.PageSize, newsArticles.Count);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto dto)
        {
            try
            {
                var data = await _newsArticleRepository.CreateAsync(dto);

                return Ok(data);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews(string id)
        {
            var news = await _newsArticleRepository.FindById(id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(EditNewsDto dto)
        {
            try
            {
                var result = await _newsArticleRepository.EditAsync(dto);

                return Ok(result);
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _newsArticleRepository.DeleteAsync(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
