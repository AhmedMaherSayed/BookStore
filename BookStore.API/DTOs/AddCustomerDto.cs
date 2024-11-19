using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs
{
    public class AddCustomerDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        //[RegularExpression("/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        //[RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
