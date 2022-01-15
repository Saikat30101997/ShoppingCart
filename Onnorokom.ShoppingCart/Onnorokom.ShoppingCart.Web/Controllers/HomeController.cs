using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Web.Models;
using Onnorokom.ShoppingCart.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILifetimeScope _scope;
        public HomeController(ILogger<HomeController> logger,ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<ProductModel>();
            model.GetProductsByCategory();
            return View(model);
        }

        public IActionResult AddtoCart(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
