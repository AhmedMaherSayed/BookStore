using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.CustomerDtos
{
    public class EditCustomerDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
