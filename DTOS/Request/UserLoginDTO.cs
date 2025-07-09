namespace Chat_Application.DTOS.Request
{
    public class UserLoginDTO
    {


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
