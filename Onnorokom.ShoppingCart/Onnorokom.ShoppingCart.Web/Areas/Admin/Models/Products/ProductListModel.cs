using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Products
{
    public class ProductListModel
    {
        private IProductService _productService;
        private ILifetimeScope _scope;

        public ProductListModel()
        {

        }

        public ProductListModel(IProductService productService)
        {
            _productService = productService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _productService = _scope.Resolve<IProductService>();
        }

        internal object GetProductData(DataTablesAjaxRequestModel tableModel)
        {
            var data = _productService.GetProducts(
              tableModel.PageIndex,
              tableModel.PageSize,
              tableModel.SearchText,
              tableModel.GetSortText(new string[] { "Name","CategoryName","Price","Image Name" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.CategoryName,
                                record.Price.ToString(),
                                record.ImageName,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
