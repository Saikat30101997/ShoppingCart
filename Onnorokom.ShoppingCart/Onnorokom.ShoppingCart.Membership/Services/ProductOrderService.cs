using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class ProductOrderService : IProductOrderService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public ProductOrderService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void AcceptProductOrder(int id,DateTime DeliveryDate)
        {
            var productEntity = _shoppingCartUnitOfWork.ProductOrders.GetById(id);
            var stock = _shoppingCartUnitOfWork.Stocks.Get(x => x.ProductId == productEntity.ProductId,string.Empty);

            if (productEntity != null && stock.Count>0 && stock[0].Quantity>=productEntity.Quantity)
            {
                productEntity.OrderStatus = "Confirmed";
                productEntity.DeliveryDate = DeliveryDate;

                stock[0].Id = stock[0].Id;
                if (stock[0].Quantity >= productEntity.Quantity)
                    stock[0].Quantity -= productEntity.Quantity;

                _shoppingCartUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Order is not found or Product is not in stock");
        }

        public void Create(ProductOrder productOrder)
        {
            if (productOrder == null)
                throw new InvalidOperationException("Product Order must be provided");

            var product = _shoppingCartUnitOfWork.Products.GetById(productOrder.ProductId);

            if (product == null)
                throw new InvalidOperationException("Product is not found");

            productOrder.TotalPrice = product.Price * productOrder.Quantity;

            _shoppingCartUnitOfWork.ProductOrders.Add(
                _mapper.Map<Entities.ProductOrder>(productOrder));


            _shoppingCartUnitOfWork.Save();
        }

        public ProductOrder GetOrder(int id)
        {
            var order = _shoppingCartUnitOfWork.ProductOrders.GetById(id);

            return _mapper.Map<ProductOrder>(order);
        }

        public (IList<ProductOrder> records, int total, int totalDisplay) GetProductOrders(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var orderEntityData = _shoppingCartUnitOfWork.ProductOrders.GetAll();

            var orderData = (from order in orderEntityData
                             select _mapper.Map<ProductOrder>(order)).ToList();

            foreach (var item in orderData)
            {
                var product = _shoppingCartUnitOfWork.Products.GetById(item.ProductId);
                var stock = _shoppingCartUnitOfWork.Stocks.Get(x => x.ProductId == item.ProductId, string.Empty);

                if (stock.Count == 0)
                    item.Stock = 0;
                else item.Stock = stock[0].Quantity;

                if (product != null)
                    item.ProductName = product.Name;
                else
                    item.ProductName = "NULL";
            }

            if(string.IsNullOrWhiteSpace(searchText) == false)
                orderData = (from order in orderData where order.ProductName == searchText
                             select order).ToList();

            var data = new List<ProductOrder>();

            if (sortText == null)
                data = orderData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else 
                data = orderData.AsQueryable().OrderBy(sortText).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return (data, data.Count, orderData.Count);
        }

        public (IList<ProductOrder> records, int total, int totalDisplay) GetProductOrdersForUser(Guid userId, int pageIndex, int pageSize, string searchText, string sortText)
        {
            var orderEntityData = _shoppingCartUnitOfWork.ProductOrders.GetAll();

            var orderData = (from order in orderEntityData where order.UserId == userId
                             select _mapper.Map<ProductOrder>(order)).ToList();

            foreach (var item in orderData)
            {
                var product = _shoppingCartUnitOfWork.Products.GetById(item.ProductId);

                if (product != null)
                    item.ProductName = product.Name;
                else
                    item.ProductName = "NULL";
            }

            if (string.IsNullOrWhiteSpace(searchText) == false)
                orderData = (from order in orderData
                             where order.ProductName == searchText
                             select order).ToList();

            var data = new List<ProductOrder>();

            if (sortText == null)
                data = orderData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else
                data = orderData.AsQueryable().OrderBy(sortText).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return (data, data.Count, orderData.Count);
        }

        public void Reject(int id)
        {
            var productEntity = _shoppingCartUnitOfWork.ProductOrders.GetById(id);

            if (productEntity != null)
            {
                productEntity.OrderStatus = "Rejected";
                _shoppingCartUnitOfWork.Save();
            }
        }

        public void Remove(int id)
        {
            _shoppingCartUnitOfWork.ProductOrders.Remove(id);

            _shoppingCartUnitOfWork.Save();
        }
    }
}
