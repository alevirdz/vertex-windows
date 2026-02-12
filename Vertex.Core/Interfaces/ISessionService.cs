using Vertex.Core.Models;

namespace Vertex.Core.Interfaces
{
    public interface ISessionService
    {
        string Token { get; }
        User? CurrentUser { get; }

        void SetSession(string token, User user);
        void ClearSession();
        bool IsAuthenticated { get; }
    }
}
