using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Onnorokom.ShoppingCart.Common.DataTable;
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
            var model = _scope.Resolve<StockListModel>();
            return View(model);
        }

        public JsonResult GetStockData()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<StockListModel>();
            var data = model.GetStockData(tableModel);

            return Json(data);
        }

        public IActionResult CreateStock(int id)
        {
            var model = _scope.Resolve<CreateStockModel>();
            model.ProductId = id;
            return View(model);
        }
        
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult CreateStock(CreateStockModel model)
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
                    ModelState.AddModelError(string.Empty, "Failed to create stock");
                    _logger.LogError(ex, "Stock Creation Failed");
                }
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = _scope.Resolve<EditStockModel>();
            model.LoadStockModel(id);
            return View(model);
        }

       [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Edit(EditStockModel model)
        {
            model.Resolve(_scope);
            if(ModelState.IsValid)
            {
                try
                {
                    model.Update();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Stock is not edited");
                    _logger.LogError(ex, "Stock Edition Failed");
                }
            }

            return View(model);
        }
    }
}
