using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class FindStudentModel : PageModel
    {
        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public Student EnrolledStudent { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            BCS RequestDirector = new();
            EnrolledStudent = RequestDirector.FindStudent(StudentID);

            if (EnrolledStudent != null)
            {
                Message = "Student found:";
            }
            else
            {
                Message = "No student found with the provided ID.";
            }
        }
    }
}
