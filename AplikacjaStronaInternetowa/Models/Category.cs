using System.ComponentModel.DataAnnotations;

namespace AplikacjaStronaInternetowa.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Category name must be at least 2 characters long")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
