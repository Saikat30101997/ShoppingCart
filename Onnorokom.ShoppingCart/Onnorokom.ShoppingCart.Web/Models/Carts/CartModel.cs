using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Models.Carts
{
    public class CartModel
    {
        private ILifetimeScope _scope;
        private ICartService _cartService;

        public CartModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public CartModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        internal void CreateCart(int id, Guid userId)
        {
            var cart = new Cart
            {
                ProductId = id,
                UserId = userId,
                Date = DateTime.Today
            };

            _cartService.Create(cart);
        }

        internal object GetCartData(DataTablesAjaxRequestModel tableModel, Guid userId)
        {
            var data = _cartService.Carts(
              userId,
              tableModel.PageIndex,
              tableModel.PageSize,
              tableModel.SearchText,
              tableModel.GetSortText(new string[] { "ProductName","Date" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.ProductName,
                                record.Date.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Remove(int id)
        {
            _cartService.Remove(id);
        }
    }
}
