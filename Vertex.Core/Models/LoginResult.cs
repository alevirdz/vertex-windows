using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vertex.Core.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public LoginData Data { get; set; } = null!;
    }

    public class LoginData
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("user")]
        public User User { get; set; } = null!;

        [JsonPropertyName("menu")]
        public Menu Menu { get; set; } = null!;
    }

    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("company_id")]
        public int CompanyId { get; set; }

        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; } = new();
    }

    public class Role
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class Menu
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<MenuItem> Items { get; set; } = new();
    }

    public class MenuItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("route")]
        public string Route { get; set; } = string.Empty;

        [JsonPropertyName("children")]
        public List<MenuItem> Children { get; set; } = new();
    }

}
