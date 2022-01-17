using Autofac;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Purchases
{
    public class CreatePurchaseModel
    {
        [Required]
        [StringLength(100,MinimumLength = 5)]
        public string SellerName { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 3)]
        public string ProductName { get; set; }

        private IPurchaseService _purchaseService;
        private ILifetimeScope _scope;

        public CreatePurchaseModel()
        {

        }

        public CreatePurchaseModel(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _purchaseService = _scope.Resolve<IPurchaseService>();
        }
        internal void Create()
        {
            var purchase = new Purchase
            {
                SellerName = SellerName,
                ProductName = ProductName,
                PurchaseDate = PurchaseDate,
                Quantity = Quantity,
                Price = Price
            };

            _purchaseService.Create(purchase);

        }
    }
}
