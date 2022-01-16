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
    public class CartService : ICartService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public CartService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Cart cart)
        {
            if (cart == null)
                throw new InvalidOperationException("Cart is not provided");

            _shoppingCartUnitOfWork.Carts.Add(
                _mapper.Map<Entities.Cart>(cart));

            _shoppingCartUnitOfWork.Save();
        }

    }
}
