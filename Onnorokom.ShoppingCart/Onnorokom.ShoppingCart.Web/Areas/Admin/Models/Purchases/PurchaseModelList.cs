using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Purchases
{
    public class PurchaseModelList
    {
        private IPurchaseService _purchaseService;
        private ILifetimeScope _scope;

        public PurchaseModelList()
        {

        }

        public PurchaseModelList(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _purchaseService = _scope.Resolve<IPurchaseService>();
        }

        internal object GetPurchaseData(DataTablesAjaxRequestModel tableModel)
        {
            var data = _purchaseService.GetPurchases(
             tableModel.PageIndex,
             tableModel.PageSize,
             tableModel.SearchText,
             tableModel.GetSortText(new string[] { "ProductName", "SellerName", "PurchaseDate", "Quantity","Price","TotalPrice" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.ProductName,
                                record.SellerName,
                                record.PurchaseDate.ToString(),
                                record.Quantity.ToString(),
                                record.Price.ToString(),
                                record.TotalPrice.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
