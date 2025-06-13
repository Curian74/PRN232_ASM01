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

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 10)
        {
            try
            {
                var response = await _newsService.GetNews(pageIndex, pageSize);

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

            try
            {
                await _newsService.CreateAsync(model);

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
