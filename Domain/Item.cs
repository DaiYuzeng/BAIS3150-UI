using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class Item
{
	public string ItemCode { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public decimal UnitPrice { get; set; }

	public string Status { get; set; } = string.Empty;
}
