using BusinessObjects;

namespace Repositories
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccount>> GetAccountsAsync();
        Task DeleteAcountAsync(short id);
        Task<SystemAccount> FindByEmailAndPassword(string email, string password);
    }
}
