using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface ICartService
    {
        void Create(Cart cart);
        (IList<Cart>records,int total,int totalDisplay) Carts(Guid userId, int pageIndex, 
            int pageSize, string searchText, string sortText);
        Cart GetCart(int id);
    }
}
