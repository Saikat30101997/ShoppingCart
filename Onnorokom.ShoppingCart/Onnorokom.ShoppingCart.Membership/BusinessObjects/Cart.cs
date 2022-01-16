using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.BusinessObjects
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Guid UserId { get; set; }
        public string ProductName { get; set; }

    }
}
