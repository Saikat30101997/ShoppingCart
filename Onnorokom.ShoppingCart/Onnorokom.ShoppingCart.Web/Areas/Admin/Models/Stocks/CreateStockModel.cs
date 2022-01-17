using Autofac;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks
{
    public class CreateStockModel
    {
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string ProductName { get; set; }
        private IStockService _stockService;
        private ILifetimeScope _scope;

        public CreateStockModel()
        {

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _stockService = _scope.Resolve<IStockService>();
        }

        internal void Create()
        {
            var stock = new Stock
            {
                ProductName = ProductName,
                Quantity = Quantity
            };

            _stockService.Create(stock);
        }
    }
}
