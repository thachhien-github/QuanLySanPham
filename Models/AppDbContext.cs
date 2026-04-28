using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Models;
using System.Collections.Generic;

namespace QuanLySanPham.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        // Khai báo bảng Categories
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dữ liệu mẫu cho Product
            List<Product> products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Sản phẩm 1", Price = 100 },
                new Product() { Id = 2, Name = "Sản phẩm 2", Price = 200 },
                new Product() { Id = 3, Name = "Sản phẩm 3", Price = 300 }
            };
            modelBuilder.Entity<Product>().HasData(products);

            // Dữ liệu mẫu cho Category
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, CategoryName = "Điện tử" },
                new Category() { Id = 2, CategoryName = "Thời trang" },
                new Category() { Id = 3, CategoryName = "Gia dụng" }
            );

        }
    }
}