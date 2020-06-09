using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.API.Models;

namespace SimpleShop.API.Data
{
    public interface IShoppingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> saveAll();
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<Article> GetArticle(int id);
        Task<Purchase> GetPurchase(int id);
    }
}