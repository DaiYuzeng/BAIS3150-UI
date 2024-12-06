using ydai5.TechnicalServices;

namespace ydai5.Domain
{
    public class ABCPOS
    {
        public bool CreateItem(string itemCode, string description, decimal unitPrice)
        {
            bool confirmation = false;

            Sales SaleManager = new();

            confirmation = SaleManager.AddItem(itemCode, description, unitPrice);

            return confirmation;
        }

        public bool UpdateItem(string itemCode, string description, decimal unitPrice)
        {
            bool confirmation = false;

            Sales SaleManager = new();

            confirmation = SaleManager.UpdateItem(itemCode, description, unitPrice);

            return confirmation;
        }

        public bool RemoveItem(string itemCode)
        {
            Sales SaleManager = new();

            bool confirmation = SaleManager.DeleteItem(itemCode);

            return confirmation;
        }

        public List<Item> GetAllItems()
        {
            Sales SaleManager = new();

            List<Item> items = SaleManager.GetItems();

            return items;
        }

        public Item FindItem(string itemCode)
        {
            Sales SaleManager = new();

            Item item = SaleManager.FindItem(itemCode);

            return item;
        }

        public bool AddCustomer(int customerId, string customerName, string address, string city, string province, string postalCode)
        {
            Sales SaleManager = new();
            return SaleManager.AddCustomer(customerId, customerName, address, city, province, postalCode);
        }

        public bool UpdateCustomer(int customerId, string customerName, string address, string city, string province, string postalCode)
        {
            Sales SaleManager = new();
            return SaleManager.UpdateCustomer(customerId, customerName, address, city, province, postalCode);
        }

        public Customer FindCustomer(int customerId)
        {
            Sales SaleManager = new();
            return SaleManager.FindCustomer(customerId);
        }

        public List<Customer> GetAllCustomers()
        {
            Sales SaleManager = new();
            return SaleManager.GetAllCustomers();
        }

        public bool DeleteCustomer(int customerId)
        {
            Sales SaleManager = new();
            return SaleManager.DeleteCustomer(customerId);
        }

        public int ProcessSale(Sale ABCSale)
        {
            Sales SaleManager = new();
            
            int SaleNumber = SaleManager.AddSale(ABCSale);
            
            return SaleNumber;
        }
    }
}
