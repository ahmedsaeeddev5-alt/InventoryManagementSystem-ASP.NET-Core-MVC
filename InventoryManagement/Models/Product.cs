using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        [DisplayName("The price")]
        [Range(1, 100000)]
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        [Required]
        [DisplayName("category")]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? clientfile { get; set; }


    }
}
