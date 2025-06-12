using BusinessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;

namespace Repositories
{
    public interface ISystemAccountRepository
    {
        Task<List<SystemAccountDto>> GetAccountsAsync(SystemAccountQuery query);
        Task DeleteAcountAsync(short id);
        Task<SystemAccount> FindByEmailAndPassword(string email, string password);
        Task<SystemAccountDto> CreateAsync(CreateAccountDto dto);
    }
}
