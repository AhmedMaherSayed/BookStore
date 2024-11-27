using BookStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.API.DTOs.BooksDtos
{
    public class BookDto
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
        public string AuthorName { get; set; }

        public int? CatalogId { get; set; }
        public string? CatalogName { get; set; }
    }
}
