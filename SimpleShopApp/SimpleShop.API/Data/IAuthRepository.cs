
using System.Threading.Tasks;
using SimpleShop.API.Models;

namespace SimpleShop.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Regiser(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}