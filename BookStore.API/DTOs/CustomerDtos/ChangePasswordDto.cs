using System.ComponentModel.DataAnnotations;

namespace BookStore.API.DTOs.CustomerDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }
    }
}
