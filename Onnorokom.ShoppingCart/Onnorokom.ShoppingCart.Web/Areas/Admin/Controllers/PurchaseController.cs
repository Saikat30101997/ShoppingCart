using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class PurchaseController : Controller
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly ILifetimeScope _scope;

        public PurchaseController(ILogger<PurchaseController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<PurchaseModelList>();

            return View(model);
        }

        public JsonResult GetPurchaseData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<PurchaseModelList>();
            var data = model.GetPurchaseData(tableModel);

            return Json(data);
        }

        public IActionResult CreatePurchase()
        {
            var model = _scope.Resolve<CreatePurchaseModel>();
            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult CreatePurchase(CreatePurchaseModel model)
        {
            model.Resolve(_scope);
            if(ModelState.IsValid)
            {
                try
                {
                    model.Create();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to create a purchase");
                    _logger.LogError(ex, "Purchase Creation Failed");
                }
            }

            return View(model);
        }
    }
}
