using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Models;

namespace QuanLySanPham.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách sản phẩm [cite: 36]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
