using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleShop.API.Data
{
    public class ShoppingRepository : IShoppingRepository
    {
        private readonly DataContext _context;
        public ShoppingRepository(DataContext context)
        {
            this._context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Article> GetArticle(int id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(u => u.Id == id);
            return article;
        }

        public async Task<Purchase> GetPurchase(int id)
        {
            return await _context.Purchases.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> saveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}