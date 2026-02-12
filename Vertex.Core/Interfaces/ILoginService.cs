using Vertex.Core.Models;
using System.Threading.Tasks;

namespace Vertex.Core.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        Task<bool> RefreshAuthTokenAsync();
    }
}
