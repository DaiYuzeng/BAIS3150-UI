using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ydai5.Pages
{
    public class CreateProgramModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Program Name is required!")]
        [StringLength(100, ErrorMessage = "Program Name cannot be longer than 100 characters.")]
        public string ProgramName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Program Code is required!")]
        [RegularExpression(@"^[A-Z]{4}$", ErrorMessage = "Program Code must be exactly 4 uppercase letters.")]
        public string ProgramCode { get; set; } = string.Empty;

        [BindProperty]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                Message = "Program created successfully!";
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
