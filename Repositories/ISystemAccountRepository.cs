using BusinessObjects;

namespace Repositories
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccount>> GetAccountsAsync();
    }
}
