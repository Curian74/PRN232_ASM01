﻿using DataAccessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace PhamQuocCuong_SE1821_A01_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("Paged")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories([FromQuery]CategoryQuery query)
        {
            var news = await _categoryRepository.GetAccountsAsync(query);

            var skip = (query.PageIndex - 1) * query.PageSize;

            var pagedData = news.Skip(skip).Take(query.PageSize);

            var data = new PagedResult<CategoryDto>(pagedData,
            query.PageIndex, query.PageSize, news.Count);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(short id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(UpdateCategoryDto dto)
        {
            try
            {
                var result = await _categoryRepository.UpdateAsync(dto);

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

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            try
            {
                var result = await _categoryRepository.CreateAsync(dto);
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return NoContent();
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
