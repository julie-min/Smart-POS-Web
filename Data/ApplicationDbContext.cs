using Microsoft.EntityFrameworkCore;
using SmartPOSWeb.Models;

namespace SmartPOSWeb.Data
{
    public class ApplicationDbContext : DbContext
         // DbContext의 문법을 상속받아서 사용하겠다.
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // DbContextOptions : 연결정보
            // obtions : 나의 mysql 정보 (외부)
            // base(options) : DbContext에 전달하는 방식

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
