using Autofac;
using AutoMapper;
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
        [Range(1,100 , ErrorMessage = "Quantity must be between 1 to 100")]
        public int Quantity { get; set; }
        private IStockService _stockService;
        private ILifetimeScope _scope;
        private IMapper _mapper;
        public int ProductId { get; set; }

        public CreateStockModel()
        {

        }

        public CreateStockModel(IStockService stockService,IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _stockService = _scope.Resolve<IStockService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void Create()
        {
            var stock = _mapper.Map<Stock>(this);
            stock.ProductId = ProductId;

            _stockService.Create(stock);
        }
    }
}
