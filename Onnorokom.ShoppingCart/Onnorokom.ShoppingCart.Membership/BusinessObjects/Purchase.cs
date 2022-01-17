using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.BusinessObjects
{
    public class Purchase
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string ProductName { get; set; }
    }
}
