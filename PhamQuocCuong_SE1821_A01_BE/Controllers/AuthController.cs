using DataAccessObjects.Dtos;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace PhamQuocCuong_SE1821_A01_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISystemAccountRepository _systemAccountRepository;

        public AuthController(ISystemAccountRepository systemAccountRepository)
        {
            _systemAccountRepository = systemAccountRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var userAccount = await _systemAccountRepository
                    .FindByEmailAndPassword(loginDto.Email, loginDto.Password);

                return Ok(userAccount);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
