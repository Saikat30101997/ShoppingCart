using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class ProductOrderService : IProductOrderService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public ProductOrderService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public ProductOrder GetOrder(int id)
        {
            var order = _shoppingCartUnitOfWork.ProductOrders.GetById(id);

            return _mapper.Map<ProductOrder>(order);
        }
    }
}
