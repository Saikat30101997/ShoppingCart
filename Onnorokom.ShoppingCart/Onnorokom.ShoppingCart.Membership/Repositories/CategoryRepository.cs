using Onnorokom.ShoppingCart.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Onnorokom.ShoppingCart.Data;
using Microsoft.EntityFrameworkCore;
using Onnorokom.ShoppingCart.Membership.Contexts;

namespace Onnorokom.ShoppingCart.Membership.Repositories
{
    public class CategoryRepository : Repository<Category, int> , ICategoryRepository
    {
        public CategoryRepository(IShoppingCartDbContext context) :
            base((DbContext)context)
        {

        }
    }
}
