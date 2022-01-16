using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.BusinessObjects
{
    public class ProductOrder
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public string Quantity { get; set; }
    }
}
