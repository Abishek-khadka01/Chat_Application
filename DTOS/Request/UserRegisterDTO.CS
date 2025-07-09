using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat_Application.DTOS.Request
{
    public class UserRegisterDTO
    {
        [Required]
        [MinLength(5, ErrorMessage ="The username should be of at least 5 characters")]
        [MaxLength(30, ErrorMessage ="The username cannot be greater than 30 characters")]
        public string Username { get; set; } =String.Empty;

        
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "The password should be of at least 8 characters")]
        public string Password { get; set; } = null!;

    }
}
