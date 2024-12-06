using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class DeleteItemModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Item Code is required.")]
        [StringLength(6, ErrorMessage = "Item Code must be exactly 6 characters.", MinimumLength = 6)]
        public string ItemCode { get; set; } = string.Empty;

        [BindProperty]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public decimal UnitPrice { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ItemCode))
            {
                ABCPOS ABCHardware = new();

                Item item = ABCHardware.FindItem(ItemCode);

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

                bool confirmation = ABCHardware.RemoveItem(ItemCode);

                if (confirmation)
                {
                    Message = "Item has been deleted successfully.";
                    Description = string.Empty;
                    UnitPrice = 0;
                }
                else
                {
                    Message = "Failed to delete the item.";
                }
            }
            else
            {
                Message = "Form not valid!";
            }
        }
    }
}
