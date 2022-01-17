using Onnorokom.ShoppingCart.Data;
using Onnorokom.ShoppingCart.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.UnitOfWorks
{
    public interface IShoppingCartUnitOfWork : IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICartRepository Carts { get; }
        IProductOrderRepository ProductOrders { get; }
        IStockRepository Stocks { get; }
    }
}
