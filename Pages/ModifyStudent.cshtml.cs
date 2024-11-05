using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;
using System.Text.RegularExpressions;

namespace ydai5.Pages
{
    public class ModifyStudentModel : PageModel
    {
        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet(string studentId)
        {
            // Assuming we get the StudentID from the query parameter to load student details
            StudentID = studentId;

            // Load existing student details
            BCS RequestDirector = new();
            Student EnrolledStudent = RequestDirector.FindStudent(StudentID);

            if (EnrolledStudent != null)
            {
                FirstName = EnrolledStudent.FirstName;
                LastName = EnrolledStudent.LastName;
                Email = EnrolledStudent.Email;
            }
            else
            {
                Message = "No student found with the provided ID.";
            }
        }

        public void OnPost()
        {
            BCS RequestDirector = new();
            Student EnrolledStudent = RequestDirector.FindStudent(StudentID);

            if (EnrolledStudent != null)
            {
                EnrolledStudent.FirstName = FirstName;
                EnrolledStudent.LastName = LastName;
                EnrolledStudent.Email = Email;

                bool Confirmation = RequestDirector.ModifyStudent(EnrolledStudent);

                if (Confirmation)
                {
                    Message = "Student has been modified successfully.";
                }
                else
                {
                    Message = "Failed to modify the student.";
                }
            }
            else
            {
                Message = "No student found with the provided ID.";
            }
        }
    }
}
