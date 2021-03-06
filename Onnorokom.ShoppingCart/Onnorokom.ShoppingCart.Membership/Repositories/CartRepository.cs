using Microsoft.EntityFrameworkCore;
using Onnorokom.ShoppingCart.Data;
using Onnorokom.ShoppingCart.Membership.Contexts;
using Onnorokom.ShoppingCart.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Repositories
{
    public class CartRepository : Repository<Cart,int>, ICartRepository
    {
        public CartRepository(IShoppingCartDbContext context) :
           base((DbContext)context)
        {

        }
    }
}
