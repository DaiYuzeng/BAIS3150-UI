using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain; // Assuming BCS is defined in Domain namespace

namespace ydai5.Pages
{
    public class CreateProgramModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Program Code is required.")]
        [StringLength(10, ErrorMessage = "Program Code must not exceed 10 characters.")]
        public string ProgramCode { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Program Description is required.")]
        [StringLength(100, ErrorMessage = "Program Description must not exceed 100 characters.")]
        public string Description { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();

                bool Confirmation = RequestDirector.CreateProgram(ProgramCode, Description);

                if (Confirmation)
                {
                    Message = "Program has been created successfully.";
                }
                else
                {
                    Message = "Failed to add the program.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
