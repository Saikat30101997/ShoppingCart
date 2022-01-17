using Onnorokom.ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Entities
{
    public class Purchase : IEntity<int>
    {
        public int Id { get; set; }
        public string SellerName { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
