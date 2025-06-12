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

        public async Task CreateAccountAsync(CreateAccountDto dto)
        {
            var response = await _apiService
                .PostAsync<SystemAccountDto>($"SystemAccounts", dto);
        }

        public async Task<SystemAccountDto> GetByIdAsync(short id)
        {
            var response = await _apiService
                .GetAsync<SystemAccountDto>($"SystemAccounts/{id}");

            return response;
        }

        public async Task<SystemAccountDto> UpdateAsync(EditAccountDto dto)
        {
            var response = await _apiService
                .PutAsync<SystemAccountDto>($"SystemAccounts/Edit", dto);

            return response;
        }

        public async Task<bool> Delete(short id)
        {
            var response = await _apiService
                .DeleteAsync($"SystemAccounts/{id}");

            return response;
        }
    }
}
