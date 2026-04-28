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

        // 📄 READ - Danh sách
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // ➕ CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // ➕ CREATE - POST
        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(p);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // 🔍 DETAILS
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✏️ EDIT - GET
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✏️ EDIT - POST
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(p);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        // ❌ DELETE - GET
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ❌ DELETE - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}