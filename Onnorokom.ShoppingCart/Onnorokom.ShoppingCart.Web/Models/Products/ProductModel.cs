using Autofac;
using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Models.Products
{
    public class ProductModel
    {
        private IProductService _productService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        public IList<Product> Products { get; set; }
        public ProductModel()
        {

        }

        public ProductModel(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _productService = _scope.Resolve<IProductService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal void GetProductsByCategory()
        {
            Products = _productService.GetProductsByCategory();
        }
    }
}
