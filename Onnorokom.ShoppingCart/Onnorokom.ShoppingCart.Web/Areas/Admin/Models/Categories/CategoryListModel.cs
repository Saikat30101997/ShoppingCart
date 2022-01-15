using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories
{
    public class CategoryListModel
    {
        private ICategoryService _categoryService;
        private ILifetimeScope _scope;

        public CategoryListModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _categoryService = _scope.Resolve<ICategoryService>();
        }

        public CategoryListModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        internal object GetCategoryData(DataTablesAjaxRequestModel tableModel)
        {
            var data = _categoryService.GetCategories(
               tableModel.PageIndex,
               tableModel.PageSize,
               tableModel.SearchText,
               tableModel.GetSortText(new string[] { "Name" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
