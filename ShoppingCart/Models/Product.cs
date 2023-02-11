using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage="Please enter a name")]
        public string Name { get; set; } 
        
        //Slug is the URL name of item
        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage ="Please enter description that has a minimum length of 2")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Please enter price")]
        [Column(TypeName="decimal(8, 2)")]
        public decimal Price { get; set; }

        public long CategoryId { get; set; }

        //Category will be used for the foreign key relationship
        public Category Category { get; set; }

        public string Image { get; set; }   

    }
}
