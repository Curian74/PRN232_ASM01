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

            var skip = (query.PageIndex - 1) * query.PageSize;

            var pagedData = accounts.Skip(skip).Take(query.PageSize);

            var data = new PagedResult<SystemAccountDto>(pagedData,
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountDto dto)
        {
            try
            {
                var data = await _systemAccountRepository.CreateAsync(dto);

                return Ok(data);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(short id)
        {
            var account = await _systemAccountRepository.FindById(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(EditAccountDto dto)
        {
            try
            {
                var result = await _systemAccountRepository.EditAsync(dto);

                return Ok(result);
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
