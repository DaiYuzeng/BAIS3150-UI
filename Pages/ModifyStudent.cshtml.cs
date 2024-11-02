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
            // Validate Student ID - Since it is read-only, we just check if it is still valid
            if (string.IsNullOrEmpty(StudentID))
            {
                ModelState.AddModelError("StudentID", "Student ID is required!");
            }
            else if (StudentID.Length > 10)
            {
                ModelState.AddModelError("StudentID", "Student ID must not exceed 10 characters.");
            }

            // Validate First Name
            if (string.IsNullOrEmpty(FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name is required!");
            }
            else if (FirstName.Length > 50)
            {
                ModelState.AddModelError("FirstName", "First Name must not exceed 50 characters.");
            }

            // Validate Last Name
            if (string.IsNullOrEmpty(LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required!");
            }
            else if (LastName.Length > 50)
            {
                ModelState.AddModelError("LastName", "Last Name must not exceed 50 characters.");
            }

            // Validate Email
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("Email", "Email is required!");
            }
            else if (!IsValidEmail(Email))
            {
                ModelState.AddModelError("Email", "Please enter a proper email address.");
            }

            if (ModelState.IsValid)
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
            else
            {
                Message = "Form not valid!";
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
