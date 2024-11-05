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
            BCS RequestDirector = new();

            // Attempt to find the program by code
            ActiveProgram = RequestDirector.FindProgram(ProgramCode);

            if (ActiveProgram == null)
            {
                Message = "No program found with the provided code.";
            }
            
        }
    }
}
