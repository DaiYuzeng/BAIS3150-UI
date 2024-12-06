using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydai5.Domain;

public class Customer
{
		public int CustomerID { get; set; }

		public string CustomerName { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public string City { get; set; } = string.Empty;

		public string Province { get; set; } = string.Empty;

		public string PostalCode { get; set; } = string.Empty;

		// Navigation property for related Sales
		public List<Sale> Sales { get; set; } = new List<Sale>();
}
