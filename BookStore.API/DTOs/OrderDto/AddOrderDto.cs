using BookStore.Core.Models.Enums;
using BookStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.DTOs.OrderDto
{
    public class AddOrderDto
    {
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "date")]
        public string? CustomerId { get; set; }

        public virtual ICollection<AddOrderDetailsDto> OrderDetailsList { get; set; } = new List<AddOrderDetailsDto>();
    }
}
