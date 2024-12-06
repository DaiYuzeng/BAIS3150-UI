using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ydai5.Domain;

namespace ydai5.Pages
{
    public class ProcessSaleModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Sale Date is required.")]
        public DateTime SaleDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Salesperson is required.")]
        [StringLength(25, ErrorMessage = "Salesperson name must not exceed 25 characters.")]
        public string SalesPerson { get; set; } = string.Empty;

        [BindProperty]
        public List<SaleItem> SaleItems { get; set; } = new();

        [BindProperty]
        public decimal SubTotal { get; set; }

        [BindProperty]
        public decimal GST { get; set; }

        [BindProperty]
        public decimal SaleTotal { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Customer selection is required.")]
        public int CustomerID { get; set; }

        public string Message { get; set; } = string.Empty;

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                ABCPOS ABCHardware = new();

                SubTotal = SaleItems.Sum(item => item.ItemTotal);
                SaleTotal = SubTotal * (1 + (GST / 100m));

                Sale ABCSale = new Sale
                {
                    SaleDate = SaleDate,
                    SalesPerson = SalesPerson,
                    CustomerID = CustomerID,
                    SubTotal = SubTotal,
                    GST = GST,
                    SaleTotal = SaleTotal,
                    SaleItems = SaleItems
                };

                int SaleNumber = ABCHardware.ProcessSale(ABCSale);

                if (SaleNumber > 0)
                {
                    Message = $"Sale processed successfully! Sale Number: {SaleNumber}";
                    SaleItems.Clear();
                }
                else
                {
                    Message = "Failed to process the sale.";
                }
            }
            else
            {
                Message = "Please correct the errors in the form.";
            }
        }
    }
}
