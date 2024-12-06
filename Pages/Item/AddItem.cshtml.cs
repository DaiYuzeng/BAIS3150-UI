using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Item Code is required.")]
        [StringLength(6, ErrorMessage = "Item Code must be exactly 6 characters.", MinimumLength = 6)]
        public string ItemCode { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Item Description is required.")]
        [StringLength(255, ErrorMessage = "Item Description must not exceed 255 characters.")]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                bool Confirmation = ABCHardware.CreateItem(ItemCode, Description, UnitPrice);

                if (Confirmation)
                {
                    Message = "Item has been added successfully.";
                }
                else
                {
                    Message = "Failed to add the item.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
