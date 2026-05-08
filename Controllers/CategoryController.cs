using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySanPham.Models;

namespace QuanLySanPham.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // READ - List
        public IActionResult Index(string? search, int page = 1)
        {
            const int pageSize = 5;

            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();
                query = query.Where(c => c.Name.Contains(keyword));
            }

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (totalPages <= 0) totalPages = 1;

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            var categories = query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(categories);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category c)
        {
            if (!ModelState.IsValid)
                return View(c);

            if (string.IsNullOrWhiteSpace(c.Name))
                ModelState.AddModelError(nameof(c.Name), "Tên danh mục không được để trống");

            if (!ModelState.IsValid)
                return View(c);

            _context.Categories.Add(c);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category c)
        {
            if (id != c.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(c);

            if (!_context.Categories.Any(x => x.Id == id))
                return NotFound();

            _context.Categories.Update(c);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            // Nếu category đang được Product dùng, hiển thị thông báo trên trang Delete
            bool isUsed = _context.Products.Any(p => p.CategoryId == id);
            ViewBag.IsUsed = isUsed;
            ViewBag.UsedByCount = _context.Products.Count(p => p.CategoryId == id);

            return View(category);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // chặn nếu đang được Product dùng
            bool isUsed = _context.Products.Any(p => p.CategoryId == id);
            if (isUsed)
            {
                // quay lại trang danh sách + message (đơn giản: TempData)
                TempData["DeleteError"] = "Không thể xóa danh mục vì đang được sử dụng bởi sản phẩm.";
                return RedirectToAction(nameof(Index));
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

