using BookStore.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Order : BaseEntity<int>
    {
        [Column(TypeName ="money")]
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        [Column(TypeName ="date")]
        public DateOnly OrderDate { get; set; }
        public string? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderDetails> OrderDetailsList { get; set; } = new List<OrderDetails>();
    }
}
