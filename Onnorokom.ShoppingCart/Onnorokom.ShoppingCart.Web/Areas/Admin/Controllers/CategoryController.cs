using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ILifetimeScope _scope;

        public CategoryController(ILogger<CategoryController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<CategoryListModel>();
            return View(model);
        }

        public JsonResult GetCategoryData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<CategoryListModel>();
            var data = model.GetCategoryData(tableModel);

            return Json(data);
        }

        public IActionResult CreateCategory()
        {
            var model = _scope.Resolve<CreateCategoryModel>();
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CreateCategoryModel model)
        {
            model.Resolve(_scope);

            if (model.IsCategoryAlreadyCreated(model.Name) == true)
            {
                ModelState.AddModelError(string.Empty, "Category is already created");
                return View();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    model.Create();
                }
                catch(Exception ex)
                {
                     ModelState.AddModelError(string.Empty, "Failed to create Category");
                    _logger.LogError(ex, "Category Creation Failed");
                }
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<EditCategoryModel>();
            model.LoadCategoryData(id);

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryModel model)
        {
            model.Resolve(_scope);

            if(ModelState.IsValid)
            {
                try
                {
                    model.Edit();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to edit Category");
                    _logger.LogError(ex, "Category edition Failed");
                }
            }

            return View(model);
        }
    }
}
