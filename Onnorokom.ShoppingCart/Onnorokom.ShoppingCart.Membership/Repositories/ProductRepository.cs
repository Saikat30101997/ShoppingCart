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
    public class ProductRepository : Repository<Product,int> , IProductRepository
    {
        public ProductRepository(IShoppingCartDbContext context) : base((DbContext)context)
        {

        }
    }
}
