using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace ydai5.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; } = string.Empty;
        [BindProperty]
        public string LastName { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Program { get; set; } = string.Empty;
        [BindProperty]
        public string UserID { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Validate First Name
            if (string.IsNullOrEmpty(FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name is required!");
            }

            // Validate Last Name
            if (string.IsNullOrEmpty(LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required!");
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

            // Validate Program
            if (string.IsNullOrEmpty(Program))
            {
                ModelState.AddModelError("Program", "Selecting a program is required!");
            }

            // Validate User ID
            if (string.IsNullOrEmpty(UserID))
            {
                ModelState.AddModelError("UserID", "User ID is required!");
            }
            else if (!IsValidUserID(UserID))
            {
                ModelState.AddModelError("UserID", "User ID must be 4 letters followed by 4 digits!");
            }

            // Validate Password
            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Password is required!");
            }
            else if (Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be at least 6 characters long!");
            }

            // Validate Confirm Password
            if (string.IsNullOrEmpty(ConfirmPassword) || ConfirmPassword != Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match!");
            }

            // Set the message based on validation result
            foreach (var state in ModelState)
        {
            if (state.Value.Errors.Count > 0)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    Message += $"\nKey: {state.Key}, Error: {error.ErrorMessage}";
                }
            }
        }
            if (ModelState.IsValid)
            {
                Message = "Welcome!";
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

        private bool IsValidUserID(string userId)
        {
            string pattern = @"^[A-Za-z]{4}\d{4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(userId);
        }
    }
}
