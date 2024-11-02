using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ydai5.Domain;
using System.Text.RegularExpressions;

namespace ydai5.Pages
{
    public class EnrollStudentModel : PageModel
    {
        [BindProperty]
        public string StudentID { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string ProgramCode { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

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

            // Validate Program Code
            if (string.IsNullOrEmpty(ProgramCode))
            {
                ModelState.AddModelError("ProgramCode", "Program Code is required!");
            }
            else if (ProgramCode.Length > 10)
            {
                ModelState.AddModelError("ProgramCode", "Program Code must not exceed 10 characters.");
            }

            // Set the message based on validation result
            if (ModelState.IsValid)
            {
                BCS RequestDirector = new();
                Student AcceptedStudent = new()
                {
                    StudentID = StudentID,
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email
                };

                bool Confirmation = RequestDirector.EnrollStudent(AcceptedStudent, ProgramCode);

                if (Confirmation)
                {
                    Message = "Student has been enrolled successfully.";
                }
                else
                {
                    Message = "Failed to enroll the student.";
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
