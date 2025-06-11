using BusinessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects;
using DataAccessObjects.Queries;
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
        public async Task<IActionResult> GetAccounts([FromQuery]SystemAccountQuery query)
        {
            var accounts = await _systemAccountRepository.GetAccountsAsync(query);

            var data = new PagedResult<SystemAccountDto>(accounts,
            query.PageIndex, query.PageSize, accounts.Count);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(short id)
        {
            try
            {
                await _systemAccountRepository.DeleteAcountAsync(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
