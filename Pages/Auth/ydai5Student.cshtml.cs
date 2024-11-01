using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ydai5.Auth
{
    public class ydai5StudentModel : PageModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Program is required.")]
        [StringLength(10, ErrorMessage = "Program cannot be longer than 10 characters.")]
        public string Program { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required.")]
        [StringLength(8, ErrorMessage = "User ID must be 8 characters long.")]
        [RegularExpression(@"[A-Z]{4}[0-9]{4}", ErrorMessage = "User ID must follow the pattern: 4 letters followed by 4 digits.")]
        public string UserID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be between 6 and 100 characters.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}


