using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class SaleItem
{
	public int SaleNumber { get; set; }

	public string ItemCode { get; set; } = string.Empty;

	public int Quantity { get; set; }

	public decimal ItemTotal { get; set; }
}
