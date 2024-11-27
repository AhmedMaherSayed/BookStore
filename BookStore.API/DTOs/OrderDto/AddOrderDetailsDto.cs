using BookStore.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.API.DTOs.OrderDto
{
    public class AddOrderDetailsDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
