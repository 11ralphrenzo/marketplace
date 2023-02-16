using marketplace.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        { 
        
        }
    }
}
