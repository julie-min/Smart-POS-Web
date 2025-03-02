using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSWeb.Models
{
    public class Inventory
    {
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("avg_sales")]
        public int AvgSales { get; set; }
        [Column("last_restock_date")]
        public DateTime LastRestockDate { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

    }
}
