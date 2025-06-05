using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace PhamQuocCuong_SE1821_A01_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAccountsController : ControllerBase
    {
        private readonly ISystemAccountRepository _systemAccountRepository;

        public SystemAccountsController(ISystemAccountRepository systemAccountRepository)
        {
            _systemAccountRepository = systemAccountRepository;
        }

        [HttpGet]   
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _systemAccountRepository.GetAccountsAsync());
        }
    }
}
