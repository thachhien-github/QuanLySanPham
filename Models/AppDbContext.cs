using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

            // Configure 1-n relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data removed to avoid duplicate key issues when migrations were partially applied.

        }
    }
}
