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

        public Student? EnrolledStudent { get; set; } = null;

        public void OnGet(string studentId)
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                BCS RequestDirector = new();
                EnrolledStudent = RequestDirector.FindStudent(studentId);
                Console.WriteLine(EnrolledStudent.StudentID);

                if (EnrolledStudent == null)
                {
                    Message = "Student not found.";
                }
                else
                {
                    StudentID = studentId;
                }
            }
        }

        public void OnPost()
        {
            BCS RequestDirector = new();

            Console.WriteLine(StudentID);

            bool Confirmation = RequestDirector.RemoveStudent(StudentID);

            if (Confirmation)
            {
                Message = "Student has been removed successfully.";
                EnrolledStudent = null; // Clear the FoundStudent to remove the details from the screen
            }
            else
            {
                Message = "Failed to remove the student.";
            }
        }
    }
}
