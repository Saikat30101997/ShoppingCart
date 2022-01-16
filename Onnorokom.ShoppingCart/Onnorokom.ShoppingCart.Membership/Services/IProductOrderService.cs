using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface IProductOrderService
    {
        ProductOrder GetOrder(int id);
        void Create(ProductOrder productOrder);
    }
}
