using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class AddCustomerDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
