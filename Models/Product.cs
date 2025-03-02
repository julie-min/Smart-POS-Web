using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSWeb.Models
{
    public class Product
    {
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public required string ProductName { get; set; }
        [Column("cost_price")]
        public decimal CostPrice { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

    }
}
