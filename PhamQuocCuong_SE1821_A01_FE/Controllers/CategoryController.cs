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

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 10)
        {
            var result = await _categorySerivce.GetCategoriesAsync(pageIndex, pageSize);

            return View(result);
        }
    }
}
