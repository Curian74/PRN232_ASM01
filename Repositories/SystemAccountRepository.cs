using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly IConfiguration _configuration;

        public SystemAccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SystemAccountDto> CreateAsync(CreateAccountDto dto)
        {
            return await SystemAccountDAO.Create(dto);
        }

        public async Task DeleteAcountAsync(short id)
        {
            await SystemAccountDAO.DeleteAccount(id);
        }

        public Task<SystemAccount> FindByEmailAndPassword(string email, string password)
        {
            return SystemAccountDAO.FindUserByEmailAndPassword(email, password, _configuration);
        }

        public Task<List<SystemAccountDto>> GetAccountsAsync(SystemAccountQuery query)
        {
            return SystemAccountDAO.GetAccounts(query);
        }
    }
}
