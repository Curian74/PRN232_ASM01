using Microsoft.AspNetCore.Mvc;
using PhamQuocCuong_SE1821_A01_FE.Models;
using PhamQuocCuong_SE1821_A01_FE.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PhamQuocCuong_SE1821_A01_FE.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsArticleService _newsArticleService;

        public HomeController(NewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 10)
        {
            try
            {
                var response = await _newsArticleService.GetNews(pageIndex, pageSize, true);

                return View(response);
            }

            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
