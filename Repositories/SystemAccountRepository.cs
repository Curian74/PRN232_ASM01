using BusinessObjects;
using DataAccessObjects;
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

        public async Task DeleteAcountAsync(short id)
        {
            await SystemAccountDAO.DeleteAccount(id);
        }

        public Task<SystemAccount> FindByEmailAndPassword(string email, string password)
        {
            return SystemAccountDAO.FindUserByEmailAndPassword(email, password, _configuration);
        }

        public Task<List<SystemAccount>> GetAccountsAsync()
        {
            return SystemAccountDAO.GetAccounts();
        }
    }
}
