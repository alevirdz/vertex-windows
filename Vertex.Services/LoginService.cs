using Vertex.Core.Interfaces;
using Vertex.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vertex.Services.ApiService;

namespace Vertex.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient = HttpClientProvider.Instance;
        private readonly ISessionService _sessionService;
        private string _authToken = string.Empty;

        public LoginService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("login", new { email, password });
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginResponse?.Success == true && loginResponse.Data != null)
            {
                _authToken = loginResponse.Data.Token;
                _sessionService.SetSession(_authToken, loginResponse.Data.User);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authToken);
            }

            return loginResponse ?? new LoginResponse { Success = false, Message = "Invalid response" };
        }

        public async Task<bool> RefreshAuthTokenAsync()
        {
            if (string.IsNullOrEmpty(_authToken)) return false;

            var response = await _httpClient.PostAsync("auth/refresh", null);
            var refreshResult = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (refreshResult?.Success == true && refreshResult.Data != null)
            {
                _authToken = refreshResult.Data.Token;
                _sessionService.SetSession(_authToken, refreshResult.Data.User ?? _sessionService.CurrentUser!);
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authToken);
            }

            return refreshResult?.Success ?? false;
        }
    }
}
