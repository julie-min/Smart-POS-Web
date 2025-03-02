using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPOSWeb.Data;
using SmartPOSWeb.Models;

namespace SmartPOSWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 10;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int totalProducts = _context.Products
               .Where(p => p.DeletedAt == null)
               .Count();

            var products = await _context.Products
                .Where(p => p.DeletedAt == null)
                .OrderBy(p => p.ProductId) // 정렬
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewData["TotalPages"] = (int)System.Math.Ceiling((double)totalProducts / PageSize); // 총 페이지 수 계산
            ViewData["CurrentPage"] = page; // 현재 페이지 번호

            return View(products);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product editedProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(editedProduct);
            }

            var existingProduct = _context.Products.Find(editedProduct.ProductId);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = editedProduct.ProductName;
            existingProduct.CostPrice = editedProduct.CostPrice;
            existingProduct.Price = editedProduct.Price;
            existingProduct.Description = editedProduct.Description;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            product.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetProducts(int page = 1)
        {
            var totalProducts = _context.Products.Count(p => p.DeletedAt == null);
            var totalPages = (int)Math.Ceiling((double)totalProducts / PageSize);

            var products = _context.Products
                .Where(p => p.DeletedAt == null)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Price
                }).ToList();

            return Json(new { products, totalPages, currentPage = page });
        }


    }
}
