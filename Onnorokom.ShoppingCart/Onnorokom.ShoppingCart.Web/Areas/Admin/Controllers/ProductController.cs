using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ILifetimeScope _scope;

        public ProductController(ILogger<ProductController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<ProductListModel>();
            return View(model);
        }

        public JsonResult GetProductData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var  model = _scope.Resolve<ProductListModel>();
            var data = model.GetProductData(tableModel);
            return Json(data);
        }

        public IActionResult CreateProduct()
        {
            var model = _scope.Resolve<CreateProductModel>();
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult CreateProduct(CreateProductModel model)
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
                    ModelState.AddModelError(string.Empty, "Failed to create product");
                    _logger.LogError(ex, "product Creation Failed");
                }
            }

            return View(model);
        }
    }
}
