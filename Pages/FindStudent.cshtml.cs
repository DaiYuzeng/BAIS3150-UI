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
                // Assuming BCS is defined in Domain and has a method to find a student by ID
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
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
