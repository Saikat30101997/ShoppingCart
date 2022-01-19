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
                var product = _shoppingCartUnitOfWork.Products.GetById(cart.ProductId);
                if (product != null)
                    cart.ProductName = product.Name;
                else
                    cart.ProductName = "NULL";
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

            var cartData = _shoppingCartUnitOfWork.Carts.Get(x => x.ProductId == cart.ProductId && x.Date == cart.Date,string.Empty);
            if (cartData.Count == 0)
            {
                _shoppingCartUnitOfWork.Carts.Add(
                    _mapper.Map<Entities.Cart>(cart));

                _shoppingCartUnitOfWork.Save();
            }
        }

        public Cart GetCart(int id)
        {
            var cart = _shoppingCartUnitOfWork.Carts.GetById(id);

            return _mapper.Map<Cart>(cart);
        }

        public void Remove(int id)
        {
            _shoppingCartUnitOfWork.Carts.Remove(id);
            _shoppingCartUnitOfWork.Save();
        }

        public void RemoveCart(Guid userId, int productId)
        {
            var carts = _shoppingCartUnitOfWork.Carts.Get(x => x.UserId == userId && x.ProductId == productId,string.Empty);

            var id = carts[0].Id;
            _shoppingCartUnitOfWork.Carts.Remove(id);
            _shoppingCartUnitOfWork.Save();
        }
    }
}
