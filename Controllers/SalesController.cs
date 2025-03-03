using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartPOSWeb.Data;

namespace SmartPOSWeb.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // daily sales
        [HttpGet]
        public async Task<JsonResult> GetSalesData()
        {
            var startDate = DateTime.UtcNow.Date.AddDays(-5);
            var endDate = DateTime.UtcNow.Date;

            var salesData = await _context.Orders
                .Where(o => o.CreatedAt.Date >= startDate && o.CreatedAt.Date <= endDate)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalSales = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

            var formattedSalesData = salesData.Select(s => new
            {
                Date = s.Date.ToString("MM-dd"), // 여기서 변환
                TotalSales = s.TotalSales
            });

            return Json(formattedSalesData);
        }

        // customer sales
        [HttpGet]
        public async Task<JsonResult> GetCustomerSalesData()
        {
            var customerSales = await _context.Orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key, 
                    TotalSales = g.Sum(o => o.TotalPrice)
                })
                .ToListAsync();

            return Json(customerSales);
        }


        // product sales
        public async Task<JsonResult> GetProductSalesData()
        {
            var productSales = await _context.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => new { od.ProductId, od.Product.ProductName })
                .Select(g => new
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalSales = g.Sum(od => od.Quantity * od.Price)
                })
                .OrderByDescending(g => g.TotalSales)
                .Take(10)
                .ToListAsync();

            return Json(productSales);
        }


    }
}
