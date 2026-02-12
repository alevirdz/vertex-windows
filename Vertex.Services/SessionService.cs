using Vertex.Core.Interfaces;
using Vertex.Core.Models;

namespace Vertex.Services
{
    public class SessionService : ISessionService
    {
        public string Token { get; private set; } = string.Empty;
        public User? CurrentUser { get; private set; }
        public bool IsAuthenticated => !string.IsNullOrWhiteSpace(Token);

        public void SetSession(string token, User user)
        {
            Token = token;
            CurrentUser = user;
        }

        public void ClearSession()
        {
            Token = string.Empty;
            CurrentUser = null;
        }
    }
}
