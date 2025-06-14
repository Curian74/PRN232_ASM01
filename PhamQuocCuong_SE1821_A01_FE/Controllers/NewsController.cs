using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhamQuocCuong_SE1821_A01_FE.Services;
using System.Threading.Tasks;

namespace PhamQuocCuong_SE1821_A01_FE.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsArticleService _newsService;
        private readonly CategoryService _categoryService;

        public NewsController(NewsArticleService newsService, CategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(bool? isActive = true, int? pageIndex = 1, int? pageSize = 5,
            string? searchterm = null, short? createdById = null)
        {
            try
            {
                var response = await _newsService.GetNews(pageIndex, pageSize, isActive, searchterm, createdById);
                return View(response);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public async Task<IActionResult> Create()
        {
            var cats = await _categoryService.GetAllAsync();

            ViewBag.CategoryList = cats.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cats = await _categoryService.GetAllAsync();

            ViewBag.CategoryList = cats.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }).ToList();

            try
            {
                await _newsService.CreateAsync(model);

                TempData["success"] = "Create successfully!";

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var news = await _newsService.GetByIdAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            var cats = await _categoryService.GetAllAsync();

            ViewBag.CategoryList = cats.Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.CategoryId.ToString()
            }).ToList();

            var modelType = new EditNewsDto
            {
                NewsArticleId = news.NewsArticleId,
                CategoryId = news.CategoryId,
                CreatedById = news.CreatedById,
                Headline = news.Headline,
                NewsContent = news.NewsContent,
                NewsSource = news.NewsSource,   
                NewsStatus = news.NewsStatus,
                NewsTitle = news.NewsTitle,
            };

            return View(modelType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditNewsDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _newsService.UpdateAsync(model);

                TempData["success"] = "Update successfully!";

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _newsService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
