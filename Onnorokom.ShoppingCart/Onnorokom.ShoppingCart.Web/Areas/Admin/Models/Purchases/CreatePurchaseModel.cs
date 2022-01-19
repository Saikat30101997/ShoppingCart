using Autofac;
using AutoMapper;
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
        [Required(ErrorMessage = " Please Enter Seller Name")]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Seller name characters must be between 5 to 100 characters")]
        public string SellerName { get; set; }
        [Required(ErrorMessage = "Please Enter Purchase Date")]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "Please enter Quantity")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity")]
        [Range(1,100,ErrorMessage = "Quantity must be between 1 to 100")]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "Product Name must be 3 to 100 characters")]
        public string ProductName { get; set; }

        private IPurchaseService _purchaseService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public CreatePurchaseModel()
        {

        }

        public CreatePurchaseModel(IPurchaseService purchaseService, IMapper mapper)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _purchaseService = _scope.Resolve<IPurchaseService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        internal void Create()
        {
            var purchase = _mapper.Map<Purchase>(this);

            _purchaseService.Create(purchase);

        }
    }
}
