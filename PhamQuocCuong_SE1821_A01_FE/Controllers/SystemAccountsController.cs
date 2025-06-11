using Microsoft.AspNetCore.Mvc;
using PhamQuocCuong_SE1821_A01_FE.Services;
using System.Threading.Tasks;

namespace PhamQuocCuong_SE1821_A01_FE.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly SystemAccountService _accountService;

        public SystemAccountsController(SystemAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 10)
        {
            var result = await _accountService.GetAccountsAsync(pageIndex, pageSize);

            return View(result);
        }
    }
}
