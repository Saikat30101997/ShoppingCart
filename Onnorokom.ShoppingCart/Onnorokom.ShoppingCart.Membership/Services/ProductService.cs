using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class ProductService : IProductService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product must be provided");

            var categoryentity = _shoppingCartUnitOfWork.Categories.Get(x => x.Name == product.CategoryName,null);
            var categoryId = categoryentity[0].Id;

            product.CategoryId = categoryId;

            _shoppingCartUnitOfWork.Products.Add(
                _mapper.Map<Entities.Product>(product));

            _shoppingCartUnitOfWork.Save();
        }
    }
}
