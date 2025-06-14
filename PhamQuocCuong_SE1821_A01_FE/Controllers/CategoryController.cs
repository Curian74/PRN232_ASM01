using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using PhamQuocCuong_SE1821_A01_FE.Services;

namespace PhamQuocCuong_SE1821_A01_FE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categorySerivce;

        public CategoryController(CategoryService categorySerivce)
        {
            _categorySerivce = categorySerivce;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 5, string? searchterm = null)
        {
            var result = await _categorySerivce.GetCategoriesAsync(pageIndex, pageSize, searchterm);

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _categorySerivce.CreateAsync(model);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(short id)
        {
            var result = await _categorySerivce.Delete(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(short id)
        {
            var category = await _categorySerivce.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var dto = new UpdateCategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDesciption = category.CategoryDesciption,
                IsActive = category.IsActive,
                ParentCategoryId = category.ParentCategoryId,
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _categorySerivce.UpdateAsync(model);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }
    }
}
