using DataAccessObjects.Dtos;
using DataAccessObjects;
using PhamQuocCuong_SE1821_A01_FE.ApiServices;

namespace PhamQuocCuong_SE1821_A01_FE.Services
{
    public class SystemAccountService
    {
        private readonly IApiService _apiService;

        public SystemAccountService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<PagedResult<SystemAccountDto>> GetAccountsAsync(int? pageIndex, int? pageSize)
        {
            var response = await _apiService
                .GetAsync<PagedResult<SystemAccountDto>>($"SystemAccounts?PageIndex={pageIndex}&PageSize={pageSize}");

            return response;
        }
    }
}
