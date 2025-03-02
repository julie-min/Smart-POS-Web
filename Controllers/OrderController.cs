using Microsoft.AspNetCore.Mvc;
using SmartPOSWeb.Data;
using SmartPOSWeb.Models;

namespace SmartPOSWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderRequest request)
        {
           var transaction = _context.Database.BeginTransaction();

            var newOrder = new Order
            {
                CustomerId = request.CustomerId,
                OrderStatus = 1,
                TotalPrice = 0,
                CreatedAt = DateTime.Now
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            decimal totalPrice = 0;

            foreach (var item in request.OrderItems)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (product == null)
                {
                    transaction.Rollback();
                    return BadRequest(new { message = $"Product ID {item.ProductId} not found." });
                }

                decimal itemTotal = product.Price * item.Quantity;
                totalPrice += itemTotal;

                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    CreatedAt = DateTime.Now
                };

                _context.OrderDetails.Add(orderDetail);
            }

            foreach (var item in request.OrderItems)
            {
                var inventory = _context.Inventory.FirstOrDefault(i => i.ProductId == item.ProductId);

                if (inventory != null && inventory.Quantity >= item.Quantity)
                {
                    inventory.Quantity -= item.Quantity;
                    inventory.UpdatedAt = DateTime.Now; // UpdatedAt 컬럼 업데이트

                    _context.Inventory.Update(inventory); // 변경 사항을 추적하도록 설정
                }
                else
                {
                    transaction.Rollback();
                    return BadRequest(new { message = $"Not enough stock for product ID {item.ProductId}" });
                }
            }

            newOrder.TotalPrice = totalPrice;
            _context.SaveChanges(); // 변경 사항 저장

            transaction.Commit(); // 트랜잭션 커밋

            return Ok(new { message = "Order placed successfully", orderId = newOrder.OrderId });


        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
