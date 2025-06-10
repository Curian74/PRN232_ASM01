using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public async Task DeleteAcountAsync(short id)
        {
            await SystemAccountDAO.DeleteAccount(id);
        }

        public Task<SystemAccount> FindByEmailAndPassword(string email, string password)
        {
            return SystemAccountDAO.FindUserByEmailAndPassword(email, password);
        }

        public Task<List<SystemAccount>> GetAccountsAsync()
        {
            return SystemAccountDAO.GetAccounts();
        }
    }
}
