using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class Sale
{
	public int SaleNumber { get; set; }

	public DateTime SaleDate { get; set; }

	public string SalesPerson { get; set; } = string.Empty;

	public decimal SubTotal { get; set; }

	public decimal GST { get; set; }

	public decimal SaleTotal { get; set; }

	public int CustomerID { get; set; }

	public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
