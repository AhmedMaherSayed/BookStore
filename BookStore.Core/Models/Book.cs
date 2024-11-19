using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Book : BaseEntity<int>
    {
        [StringLength(150)]
        public string Title { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PublishedDate { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int? CatalogId { get; set; }
        public Catalog? Catalog { get; set; }
        public virtual ICollection<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();
    }
}
