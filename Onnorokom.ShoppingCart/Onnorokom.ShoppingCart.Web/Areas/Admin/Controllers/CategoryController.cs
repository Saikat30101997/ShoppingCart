﻿using Autofac;
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
            if(ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
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
    }
}
