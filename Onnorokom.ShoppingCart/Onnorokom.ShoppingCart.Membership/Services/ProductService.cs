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


            if (product != null)
            {
                _shoppingCartUnitOfWork.Products.Add(
                  new Entities.Product
                  {
                      Name = product.Name,
                      CategoryId = product.CategoryId,
                      Price = product.Price,
                      ImageName = product.ImageName,
                      Description = product.Description
                  });

                _shoppingCartUnitOfWork.Save();
            }
        }

        public void Delete(int id)
        {
            _shoppingCartUnitOfWork.Products.Remove(id);

            _shoppingCartUnitOfWork.Save();
        }

        public Product GetProduct(int id)
        {
            var productEntity = _shoppingCartUnitOfWork.Products.GetById(id);
            var categoryEnitity = _shoppingCartUnitOfWork.Categories.GetById(productEntity.CategoryId);

            var product = _mapper.Map<Product>(productEntity);

            product.CategoryName = categoryEnitity.Name;

            return product;
        }

        public int GetProductId(string name)
        {
            var productEntity = _shoppingCartUnitOfWork.Products.Get(x => x.Name == name, string.Empty);

            var product = new Product();

            product.Id = productEntity[0].Id;

            return product.Id;
        }

        public (IList<Product> records, int total, int totalDisplay) GetProducts(int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            var productData = _shoppingCartUnitOfWork.Products.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? null :
                x => x.Name.Contains(searchText), sortText, string.Empty, pageIndex, pageSize);

            var data = (from product in productData.data
                        select _mapper.Map<Product>(product)).ToList();

            for(int i=0;i<data.Count;i++)
            {
                var category = _shoppingCartUnitOfWork.Categories.GetById(data[i].CategoryId);
                data[i].CategoryName = category.Name;
            }

            return (data, productData.total, productData.totalDisplay);
        }

        public IList<Product> GetProductsByCategory()
        {
            IList<Entities.Product> productEntities = _shoppingCartUnitOfWork.Products.GetAll();

            var productBO = (from product in productEntities
             select _mapper.Map<Product>(product)).ToList();

            foreach (var product in productBO)
            {
                var stock = _shoppingCartUnitOfWork.Stocks.Get(x => x.ProductId == product.Id, string.Empty);
                if (stock.Count == 0 || stock[0].Quantity<=0)
                    product.StockMessage = "Stock Out";
                else
                    product.StockMessage = "Stock In";
            }

            return productBO;
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new InvalidOperationException("Product is not provided");

            var productEntity = _shoppingCartUnitOfWork.Products.GetById(product.Id);

            var categoryEntity = _shoppingCartUnitOfWork.Categories.Get(x => x.Name == product.CategoryName, string.Empty);
            product.CategoryId = categoryEntity[0].Id;

            if (productEntity != null)
            {
                productEntity.Id = product.Id;
                productEntity.Name = product.Name;
                productEntity.ImageName = product.ImageName;
                productEntity.Price = product.Price;
                productEntity.CategoryId = product.CategoryId;
                _shoppingCartUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Product is not updated");

        }
    }
}
