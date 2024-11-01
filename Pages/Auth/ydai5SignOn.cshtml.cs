using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ydai5.Auth
{
    public class ydai5SignOnModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "User ID is required.")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{4}$", ErrorMessage = "User ID must be 4 letters followed by 4 digits (e.g. BAIS9999).")]
        public string UserID { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            Console.WriteLine($"UserID from form: '{UserID}'");
            Console.WriteLine($"Password from form: '{Password}'");

            // Check if ModelState is valid
            if (ModelState.IsValid)
            {
                Message = "*** OnGet *** - Valid";
            }
            else
            {
                Message = "*** OnGet *** - Not Valid";
            }
        }
    }
}
