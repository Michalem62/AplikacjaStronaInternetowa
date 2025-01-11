using System.ComponentModel.DataAnnotations;

namespace AplikacjaStronaInternetowa.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        public string Username { get; set; } 

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } 

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public string Role { get; set; } = "Customer"; // Default role is Customer

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
