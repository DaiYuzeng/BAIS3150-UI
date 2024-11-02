using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class RemoveStudentModel : PageModel
    {
        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet(string studentId)
        {
            StudentID = studentId;
        }

        public void OnPost()
        {
            // Validate Student ID
            if (string.IsNullOrEmpty(StudentID))
            {
                ModelState.AddModelError("StudentID", "Student ID is required!");
            }
            else if (StudentID.Length > 10)
            {
                ModelState.AddModelError("StudentID", "Student ID must not exceed 10 characters.");
            }

            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();

                bool Confirmation = RequestDirector.RemoveStudent(StudentID);

                if (Confirmation)
                {
                    Message = "Student has been removed successfully.";
                }
                else
                {
                    Message = "Failed to remove the student.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
