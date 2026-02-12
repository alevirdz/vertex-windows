using System.Net.Http.Headers;
using System.Net.Http.Json;
using Vertex.Core.Interfaces;
using Vertex.Core.Models;

namespace Vertex.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;
        private string _authToken = string.Empty;

        public LoginService(HttpClient httpClient, ISessionService sessionService)
        {
            _httpClient = httpClient;
            _sessionService = sessionService;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("login", new { email, password });

                if (!response.IsSuccessStatusCode)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = $"HTTP Error: {response.StatusCode}"
                    };
                }

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
            catch (HttpRequestException) {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Unable to connect to server"
                };
            }
                
        }

        public async Task<bool> RefreshAuthTokenAsync()
        {
            try
            {
                var currentToken = _sessionService.Token;
                if (string.IsNullOrWhiteSpace(currentToken)) return false;

                var response = await _httpClient.PostAsync("refresh", null);
                if (!response.IsSuccessStatusCode) return false;

                var refreshResult = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (refreshResult?.Success != true || refreshResult.Data == null)
                    return false;

                var newToken = refreshResult.Data.Token;
                var user = refreshResult.Data.User ?? _sessionService.CurrentUser;

                if (string.IsNullOrWhiteSpace(newToken) || user == null)
                    return false;

                _sessionService.SetSession(newToken, user);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("logout", null);
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                
            }
            finally
            {
                _sessionService.ClearSession();
                _authToken = string.Empty;
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

    }
}
