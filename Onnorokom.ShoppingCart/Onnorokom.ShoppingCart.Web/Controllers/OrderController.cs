using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Entities;
using Onnorokom.ShoppingCart.Web.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<OrderController> _logger;
        private ILifetimeScope _scope;

        public OrderController(ILogger<OrderController> logger, ILifetimeScope scope, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _scope = scope;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var model = _scope.Resolve<OrderModel>();
            return View(model);
        }

        public JsonResult GetOrderDataForUser()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.UserId;
            Guid userId = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<OrderModel>();
            var data = model.GetOrderDataForUser(tableModel,userId);

            return Json(data);
        }

        public IActionResult OrderProduct(int id)
        {
            var model = _scope.Resolve<OrderModel>();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderProduct(OrderModel model)
        {
            model.Resolve(_scope);
            if(ModelState.IsValid)
            {
                try
                {
                    ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
                    string s = ViewBag.UserId;
                    Guid userId = Guid.Parse(s);
                    var user =await _userManager.GetUserAsync(HttpContext.User);
                    string email = Convert.ToString(user);
                    model.Create(userId, email);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Order is not created");
                    _logger.LogError(ex, "Order Creation Failed");
                }
            }

            return View(model);
        }

        public IActionResult CancelProductOrder(int id)
        {
            var model = _scope.Resolve<OrderModel>();
            model.CancelProductOrder(id);
            return View(model);
        }

        public IActionResult CancelOrder(int id)
        {
            var model = _scope.Resolve<OrderModel>();
            model.RemoveOrder(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
