using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSWeb.Models
{
    public class OrderDetail
    {
        [Column("order_detail_id")]
        public int OrderDetailId { get; set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


        public virtual Order Order { get; set; }
        public Product Product { get; set; }
    }
}
