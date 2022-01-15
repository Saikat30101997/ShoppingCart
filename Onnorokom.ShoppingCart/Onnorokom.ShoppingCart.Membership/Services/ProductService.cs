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

            var categoryentity = _shoppingCartUnitOfWork.Categories.Get(x => x.Name == product.CategoryName,string.Empty);
            var categoryId = categoryentity[0].Id;

            product.CategoryId = categoryId;

            _shoppingCartUnitOfWork.Products.Add(
                _mapper.Map<Entities.Product>(product));

            _shoppingCartUnitOfWork.Save();
        }

        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            var productData = _shoppingCartUnitOfWork.Products.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? null :
                x => x.Name.Contains(searchText), sortText, "Category", pageIndex, pageSize);

            var data = (from product in productData.data
                        select _mapper.Map<Product>(product)).ToList();

            for(int i=0;i<data.Count;i++)
            {
                data[i].CategoryName = productData.data[0].Category.Name;
            }

            return (data, productData.total, productData.totalDisplay);
        }

        public IList<Product> GetProductsByCategory()
        {
            IList<Entities.Product> productEntities = _shoppingCartUnitOfWork.Products.GetAll();

            var productBO = (from product in productEntities
             select _mapper.Map<Product>(product)).ToList();

            return productBO;
        }
    }
}
