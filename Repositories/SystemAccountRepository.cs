using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public Task<List<SystemAccount>> GetAccountsAsync()
        {
            return SystemAccountDAO.GetAccounts();
        }
    }
}
