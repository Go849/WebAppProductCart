using System.ComponentModel.DataAnnotations;

namespace WebAppProductCart.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
