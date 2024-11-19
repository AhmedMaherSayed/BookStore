using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Models
{
    public class Catalog : BaseEntity<int>
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}