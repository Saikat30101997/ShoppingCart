using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles="Admin")]
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;
        private readonly ILifetimeScope _scope;

        public StockController(ILogger<StockController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateStock()
        {
            var model = _scope.Resolve<CreateStockModel>();

            return View(model);
        }
    }
}
