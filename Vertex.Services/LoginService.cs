using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Vertex.Core.Models;

namespace Vertex.Services.ApiService;

public interface ILoginService
{
    Task<LoginResponse> LoginAsync(string username, string password);
    Task<bool> RefreshAuthTokenAsync();
}

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient = HttpClientProvider.Instance;
    private string _authToken = string.Empty;

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        try
        {
            var loginPayload = new { email, password };
            var response = await _httpClient.PostAsJsonAsync("login", loginPayload);

            if (!response.IsSuccessStatusCode)
                return new LoginResponse { Success = false, Message = "HTTP error" };

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>()
                                ?? new LoginResponse { Success = false, Message = "Invalid response" };

            if (loginResponse.Success && loginResponse.Data != null)
            {
                _authToken = loginResponse.Data.Token;
                SetAuthorizationHeader(_authToken);
            }

            return loginResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LoginAsync Error: {ex.Message}");
            return new LoginResponse { Success = false, Message = ex.Message };
        }
    }

    public async Task<bool> RefreshAuthTokenAsync()
    {
        if (string.IsNullOrEmpty(_authToken)) return false;

        try
        {
            var response = await _httpClient.PostAsync("auth/refresh", null);
            if (!response.IsSuccessStatusCode) return false;

            var refreshResult = await response.Content.ReadFromJsonAsync<LoginResponse>()
                                ?? new LoginResponse { Success = false };

            if (refreshResult.Success && refreshResult.Data != null)
            {
                _authToken = refreshResult.Data.Token;
                SetAuthorizationHeader(_authToken);
            }

            return refreshResult.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"RefreshAuthTokenAsync Error: {ex.Message}");
            return false;
        }
    }

    private void SetAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}
