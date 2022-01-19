using Onnorokom.ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Entities
{
    public class ProductOrder : IEntity<int>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserEmail { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
    }
}
