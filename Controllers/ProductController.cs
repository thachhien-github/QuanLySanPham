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

        // 📄 READ - Danh sách (LAB05: Search + Paging + Statistics)
        public IActionResult Index(string search, int page = 1)
        {
            const int pageSize = 5;

            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();
                query = query.Where(p => p.Name.Contains(keyword));
            }

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (totalPages <= 0)
            {
                totalPages = 1;
            }

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var products = query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Thống kê theo danh mục
            var stats = _context.Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category!.Name)
                .Select(g => new CategoryProductStatsVm
                {
                    CategoryName = g.Key,
                    ProductCount = g.Count(),
                    MaxPrice = g.Max(x => x.Price),
                    MinPrice = g.Min(x => x.Price),
                    AvgPrice = g.Average(x => x.Price),
                    TotalPrice = g.Sum(x => x.Price)
                })
                .OrderByDescending(x => x.TotalPrice)
                .ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.Stats = stats;

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