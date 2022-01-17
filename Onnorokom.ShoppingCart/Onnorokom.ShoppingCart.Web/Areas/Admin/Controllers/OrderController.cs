using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ILifetimeScope _scope;

        public OrderController(ILogger<OrderController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<OrderListModel>();
            return View(model);
        }

        public JsonResult GetOrderData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<OrderListModel>();
            var data = model.GetOrderData(tableModel);

            return Json(data);
        }

        public IActionResult AcceptProductOrder(int id)
        {
            var model = _scope.Resolve<OrderListModel>();

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult AcceptProductOrder(OrderListModel model)
        {
            model.Resolve(_scope);
            if(ModelState.IsValid)
            {
                try
                {
                    model.AcceptProductOrder();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Product Order is not Confirmed");
                    _logger.LogError(ex, "Product order confirmation failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
