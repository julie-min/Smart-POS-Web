using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPOSWeb.Models
{
    public class Customer
    {
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("customer_name")]
        public required string CustomerName {get; set;}
        [Column("contact_info")]
        public string? ContactInfo { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
