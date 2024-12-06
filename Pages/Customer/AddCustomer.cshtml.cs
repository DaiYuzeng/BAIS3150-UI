using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class AddCustomerModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(100000000, 999999999, ErrorMessage = "Customer ID must be a 9-digit integer.")]
        public int CustomerID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(25, ErrorMessage = "Customer Name must not exceed 25 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50, ErrorMessage = "Address must not exceed 50 characters.")]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(25, ErrorMessage = "City must not exceed 25 characters.")]
        public string City { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Province is required.")]
        [StringLength(25, ErrorMessage = "Province must not exceed 25 characters.")]
        public string Province { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^[A-Z]\d[A-Z] ?\d[A-Z]\d$", ErrorMessage = "Invalid Postal Code format.")]
        public string PostalCode { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                bool confirmation = ABCHardware.AddCustomer(CustomerID, CustomerName, Address, City, Province, PostalCode);

                if (confirmation)
                {
                    Message = "Customer has been added successfully.";
                }
                else
                {
                    Message = "Failed to add the customer.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
