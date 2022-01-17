using Microsoft.EntityFrameworkCore;
using Onnorokom.ShoppingCart.Data;
using Onnorokom.ShoppingCart.Membership.Contexts;
using Onnorokom.ShoppingCart.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.UnitOfWorks
{
    public class ShoppingCartUnitOfWork : UnitOfWork, IShoppingCartUnitOfWork
    {
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICartRepository Carts { get; private set; }
        public IProductOrderRepository ProductOrders { get; private set; }
        public IStockRepository Stocks { get; private set; }

        public ShoppingCartUnitOfWork(IShoppingCartDbContext context,
           IProductRepository products, ICategoryRepository categories,
           ICartRepository carts, IProductOrderRepository productOrders,
           IStockRepository stocks) :
           base((DbContext)context)
        {
            Products = products;
            Categories = categories;
            Carts = carts;
            ProductOrders = productOrders;
            Stocks = stocks;
        }
    }
}
