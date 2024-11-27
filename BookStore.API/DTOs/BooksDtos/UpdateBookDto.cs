using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.BooksDtos
{
    public class UpdateBookDto
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PublishedDate { get; set; }

        public int AuthorId { get; set; }
        public int? CatalogId { get; set; }
    }
}
