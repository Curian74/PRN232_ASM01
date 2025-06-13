using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<IActionResult> Index(int? pageIndex = 1, int? pageSize = 5)
        {
            var result = await _accountService.GetAccountsAsync(pageIndex, pageSize);

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _accountService.CreateAccountAsync(model);

                return RedirectToAction("Index", "systemAccounts");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(short id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            var modelType = new EditAccountDto
            {
                AccountEmail = account.AccountEmail,
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountPassword = account.AccountPassword,
                AccountRole = account.AccountRole,
            };

            return View(modelType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _accountService.UpdateAsync(model);

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
            var result = await _accountService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
