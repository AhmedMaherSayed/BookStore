using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.BooksDtos
{
    public class AddBookDto
    {
        [StringLength(150)]
        [Required]
        public string Title { get; set; }
        [Column(TypeName = "money")]
        [Range(20, 1000, ErrorMessage = "price range is between 20 to 1000")]
        public decimal Price { get; set; }
        [Range(1, 500, ErrorMessage = "Unit In Stock must be greater than 1 and less lthan 500")]
        public int UnitsInStock { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public int? CatalogId { get; set; }
        public DateTime? CreatedAt { get; } = DateTime.Now;

    }
}
