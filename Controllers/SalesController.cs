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

        // 🚀 일자별 매출 조회
        [HttpGet]
        public async Task<JsonResult> GetSalesData()
        {
            var salesData = await _context.OrderDetails
                .Where(o => o.CreatedAt != null) // NULL 값 방지
                .GroupBy(o => o.CreatedAt.Date) // 날짜 기준으로 그룹화
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"), // 날짜 변환
                    TotalSales = g.Sum(o => o.Quantity * o.Price) // 매출 합계
                })
                .OrderBy(g => g.Date) // 날짜순 정렬
                .ToListAsync();

            Console.WriteLine($"Sales Data Count: {salesData.Count} rows");
            foreach (var item in salesData)
            {
                Console.WriteLine($"Date: {item.Date}, Sales: {item.TotalSales}");
            }

            return Json(salesData);
        }

        // 🚀 거래처별 매출 조회
        [HttpGet]
        public async Task<JsonResult> GetCustomerSalesData()
        {
            var customerSales = await _context.OrderDetails
                .GroupBy(o => o.OrderId) // OrderId 기준으로 그룹화
                .Select(g => new
                {
                    CustomerId = g.Key, // OrderId를 고객으로 가정
                    TotalSales = g.Sum(o => o.Quantity * o.Price)
                })
                .OrderByDescending(g => g.TotalSales)
                .ToListAsync();

            return Json(customerSales);
        }

        // 🚀 상품별 매출 조회
        [HttpGet]
        public async Task<JsonResult> GetProductSalesData()
        {
            var productSales = await _context.OrderDetails
                .GroupBy(o => o.ProductId) // ProductId 기준으로 그룹화
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSales = g.Sum(o => o.Quantity * o.Price)
                })
                .OrderByDescending(g => g.TotalSales)
                .ToListAsync();

            return Json(productSales);
        }
    }
}
