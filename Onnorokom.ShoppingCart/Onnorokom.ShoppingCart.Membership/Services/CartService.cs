using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

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

        public (IList<Cart> records, int total, int totalDisplay) Carts(Guid userId, int pageIndex, int pageSize, string searchText, string sortText)
        {
            var cartEntity = _shoppingCartUnitOfWork.Carts.Get(x => x.UserId == userId, string.Empty);

            var cartData = (from cart in cartEntity
                        select _mapper.Map<Cart>(cart)).ToList();

            foreach (var cart in cartData)
            {
                var products = _shoppingCartUnitOfWork.Products.Get(x => x.Id == cart.ProductId,string.Empty);
                cart.ProductName = products[0].Name;
            }

            if (string.IsNullOrWhiteSpace(searchText) == false)
                cartData = (from cart in cartData where cart.ProductName == searchText select cart).ToList();

            IList<Cart> totalCartData = new List<Cart>();

            if (sortText == null)
                totalCartData = cartData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else
                totalCartData = cartData.AsQueryable().OrderBy(sortText).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return (totalCartData, cartData.Count, cartEntity.Count);
        }

        public void Create(Cart cart)
        {
            if (cart == null)
                throw new InvalidOperationException("Cart is not provided");

            _shoppingCartUnitOfWork.Carts.Add(
                _mapper.Map<Entities.Cart>(cart));

            _shoppingCartUnitOfWork.Save();
        }

        public Cart GetCart(int id)
        {
            var cart = _shoppingCartUnitOfWork.Carts.GetById(id);

            return _mapper.Map<Cart>(cart);
        }
    }
}
