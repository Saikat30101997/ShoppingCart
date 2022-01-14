using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.BusinessObjects
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
    }
}
