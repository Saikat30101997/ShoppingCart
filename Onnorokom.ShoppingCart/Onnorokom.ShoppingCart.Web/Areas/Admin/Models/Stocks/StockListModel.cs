using Autofac;
using AutoMapper;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks
{
    public class StockListModel
    {
        private IStockService _stockService;
        private ILifetimeScope _scope;

        public StockListModel()
        {

        }

        public StockListModel(IStockService stockService)
        {
            _stockService = stockService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _stockService = _scope.Resolve<IStockService>();
        }

        internal object GetStockData(DataTablesAjaxRequestModel tableModel)
        {
            var data = _stockService.Stocks(
              tableModel.PageIndex,
              tableModel.PageSize,
              tableModel.SearchText,
              tableModel.GetSortText(new string[] { "ProductName", "Quantity" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.ProductName,
                                record.Quantity.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
