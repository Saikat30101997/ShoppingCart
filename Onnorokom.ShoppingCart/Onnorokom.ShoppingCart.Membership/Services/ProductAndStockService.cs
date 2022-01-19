using AutoMapper;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class ProductAndStockService : IProductAndStockService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public ProductAndStockService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            _shoppingCartUnitOfWork.Products.Remove(id);

            var stock = _shoppingCartUnitOfWork.Stocks.Get(x => x.ProductId == id,string.Empty);

            if (stock.Count > 0)
                _shoppingCartUnitOfWork.Stocks.Remove(stock[0].Id);

            _shoppingCartUnitOfWork.Save();
        }
    }
}
