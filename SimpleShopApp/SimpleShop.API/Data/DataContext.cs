using SimpleShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleShop.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}