using System.ComponentModel.DataAnnotations;

namespace BookSession.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Please provide us with your firstName"), MaxLength(100)]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Please enter your first name"), MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress , MaxLength(100)]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "The format of the phone is not valid"), MaxLength(20)]
        public string PhoneNumber { get; set; } = "";

        [Required, MaxLength(200)]
        public string Address { get; set; } = "";

        [Required(ErrorMessage = "Please provide a password"), MaxLength(100)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Pleaase provide confirm password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; } = "";

    }
}
