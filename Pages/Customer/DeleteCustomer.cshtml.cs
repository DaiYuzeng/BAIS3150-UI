using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class DeleteCustomerModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(100000000, 999999999, ErrorMessage = "Customer ID must be a 9-digit integer.")]
        public int CustomerID { get; set; }

        [BindProperty]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public string City { get; set; } = string.Empty;

        [BindProperty]
        public string Province { get; set; } = string.Empty;

        [BindProperty]
        public string PostalCode { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                Customer? customer = ABCHardware.FindCustomer(CustomerID);

                if (customer != null)
                {
                    CustomerName = customer.CustomerName;
                    Address = customer.Address;
                    City = customer.City;
                    Province = customer.Province;
                    PostalCode = customer.PostalCode;
                }
                else
                {
                    Message = "Customer not found.";
                }
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                bool confirmation = ABCHardware.DeleteCustomer(CustomerID);

                if (confirmation)
                {
                    Message = "Customer has been deleted successfully.";
                    CustomerName = string.Empty;
                    Address = string.Empty;
                    City = string.Empty;
                    Province = string.Empty;
                    PostalCode = string.Empty;
                }
                else
                {
                    Message = "Failed to delete the customer.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
