using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class FindProgramModel : PageModel
    {
        [BindProperty]
        public string ProgramCode { get; set; } = string.Empty;

        public Domain.Program ActiveProgram { get; set; } = null;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Validate Program Code
            if (string.IsNullOrEmpty(ProgramCode))
            {
                ModelState.AddModelError("ProgramCode", "Program Code is required!");
            }
            else if (ProgramCode.Length > 10)
            {
                ModelState.AddModelError("ProgramCode", "Program Code must not exceed 10 characters.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();

                // Attempt to find the program by code
                ActiveProgram = RequestDirector.FindProgram(ProgramCode);

                if (ActiveProgram == null)
                {
                    Message = "No program found with the provided code.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
