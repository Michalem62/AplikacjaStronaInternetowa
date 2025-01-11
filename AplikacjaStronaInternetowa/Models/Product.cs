using System.ComponentModel.DataAnnotations;

namespace AplikacjaStronaInternetowa.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Product name must be at least 2 characters long")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
