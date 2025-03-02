namespace SmartPOSWeb.Models
{
    public class User
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
