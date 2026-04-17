using Microsoft.AspNetCore.Mvc;
using QuanLySanPham.Models;

namespace QuanLySanPham.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();

        // HIỂN THỊ DANH SÁCH
        public IActionResult Index()
        {
            return View(products);
        }

        // FORM CREATE
        public IActionResult Create()
        {
            return View();
        }

        // XỬ LÝ CREATE
        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                // Auto tăng ID
                p.Id = products.Count + 1;

                products.Add(p);
                return RedirectToAction("Index");
            }

            return View(p);
        }

        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
