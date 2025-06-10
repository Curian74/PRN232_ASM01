using BusinessObjects;
using DataAccessObjects.Dtos;
using PhamQuocCuong_SE1821_A01_FE.ApiServices;

namespace PhamQuocCuong_SE1821_A01_FE.Services
{
    public class AuthService
    {
        private readonly IApiService _apiService;

        public AuthService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<SystemAccount> Login(LoginDto loginDto)
        {
            var response = await _apiService.PostAsync<SystemAccount>($"Auth", loginDto);

            return response;
        }
    }
}
