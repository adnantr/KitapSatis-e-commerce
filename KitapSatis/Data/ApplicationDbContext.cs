  using KitapSatis.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapSatis.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }


        public DbSet<CategoryProduct> CategoryProducts { get; set; }//Çok Çok İlişki
        public DbSet<ProductKind> ProductKind { get; set; } //Çok Çok İlişki
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CategoryProduct>().HasKey(x => new { x.CategoryId, x.ProductId });
            modelBuilder.Entity<ProductKind>().HasKey(x => new { x.KindId, x.ProductId });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //     optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbKitapSatis;Trusted_Connection=True");
        //}


    }
}
