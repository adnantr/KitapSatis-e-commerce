﻿  using KitapSatis.Models;
using Microsoft.EntityFrameworkCore;

namespace KitapSatis.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<ProductKind> ProductKind { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CategoryProduct>().HasKey(x => new { x.CategoryId, x.ProductId });
            modelBuilder.Entity<ProductKind>().HasKey(x => new { x.KindId, x.ProductId });

        }
        


    }
}
