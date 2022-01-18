using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.Entities;
using Onnorokom.ShoppingCart.Web.Models.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CartController> _logger;
        private ILifetimeScope _scope;

        public CartController(ILogger<CartController> logger, ILifetimeScope scope, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _scope = scope;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<CartModel>();
            return View(model);
        }

        public JsonResult GetCartData()
        {
            ViewBag.User = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.User;
            Guid userId = Guid.Parse(s);
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<CartModel>();
            var data = model.GetCartData(tableModel,userId);

            return Json(data);
        }

        public IActionResult AddtoCart(int id)
        {
            ViewBag.User = _userManager.GetUserId(HttpContext.User);
            string s = ViewBag.User;
            Guid Id = Guid.Parse(s);
            var model = _scope.Resolve<CartModel>();
            model.CreateCart(id, Id);

            if (s == null)
                return RedirectToAction("Login", "Account");
            else
                return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<CartModel>();
            model.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
