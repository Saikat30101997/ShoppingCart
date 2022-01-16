using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Orders
{
    public class OrderListModel
    {
        private IProductOrderService _productOrderService;
        private ILifetimeScope _scope;

        public OrderListModel()
        {

        }

        public OrderListModel(IProductOrderService productOrderService)
        {
            _productOrderService = productOrderService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }

        internal object GetOrderData(DataTablesAjaxRequestModel tableModel)
        {
            var data = _productOrderService.GetProductOrders(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "UserEmail", "ProductName", "OrderDate", "DeliveryDate","OrderStatus","Quantity" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.UserEmail,
                            record.ProductName,
                            record.OrderDate.ToString(),
                            record.DeliveryDate.ToString(),
                            record.OrderStatus,
                            record.Quantity.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
