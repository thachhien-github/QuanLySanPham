using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        // ➕ CREATE - GET
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // ➕ CREATE - POST
        [HttpPost]
        public IActionResult Create(Product p)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (ModelState.IsValid && p.CategoryId > 0)
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
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        // ✏️ EDIT - GET
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            ViewBag.Categories = _context.Categories.ToList();
            if (product == null) return NotFound();

            return View(product);
        }

        // ✏️ EDIT - POST
        [HttpPost]
        public IActionResult Edit(int id, Product p)
        {
            ViewBag.Categories = _context.Categories.ToList();
            if (id != p.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid && p.CategoryId > 0)
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