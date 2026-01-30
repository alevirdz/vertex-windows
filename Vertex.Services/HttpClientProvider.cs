using System;
using System.Net.Http;

namespace Vertex.Services.ApiService;

public static class HttpClientProvider
{
    private static HttpClient? _httpClient;

    public static void Configure(string apiBaseUrl, int requestTimeoutInSeconds)
    {
        if (_httpClient != null) return;

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl),
            Timeout = TimeSpan.FromSeconds(requestTimeoutInSeconds)
        };
    }

    public static HttpClient Instance
    {
        get
        {
            if (_httpClient == null)
                throw new InvalidOperationException("HttpClientProvider not configured. Call Configure() first.");
            return _httpClient;
        }
    }
}
