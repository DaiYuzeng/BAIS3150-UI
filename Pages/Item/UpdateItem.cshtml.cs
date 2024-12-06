using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class UpdateItemModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Item Code is required.")]
        [StringLength(6, ErrorMessage = "Item Code must be exactly 6 characters.", MinimumLength = 6)]
        public string ItemCode { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255, ErrorMessage = "Description must not exceed 255 characters.")]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ItemCode))
            {
                ABCPOS ABCHardware = new();

                Item item = ABCHardware.FindItem(ItemCode);

                Console.WriteLine("----------- Find");

                if (item != null)
                {
                    Description = item.Description;
                    UnitPrice = item.UnitPrice;
                }
                else
                {
                    Message = "Item not found.";
                }
            }
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                bool confirmation = ABCHardware.UpdateItem(ItemCode, Description, UnitPrice);

                if (confirmation)
                {
                    Message = "Item has been updated successfully.";
                }
                else
                {
                    Message = "Failed to update the item.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
