using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSWeb.Models
{
    public class Order
    {
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("order_status")]
        public int OrderStatus { get; set; }
        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
