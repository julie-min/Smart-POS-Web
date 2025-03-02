using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPOSWeb.Data;
using SmartPOSWeb.Models;

namespace SmartPOSWeb.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Inventory> inventories = _context.Inventory
                                          .Include(i => i.Product)
                                          .OrderBy(i => i.ProductId)
                                          .ToList();

            //foreach (var inventory in inventories)
            //{
            //    Console.WriteLine($"Inventory ID: {inventory.InventoryId}, Product ID: {inventory.ProductId}, Product: {inventory.Product?.ProductName ?? "NULL"}");
            //}

            return View(inventories);
        }


    }
}
