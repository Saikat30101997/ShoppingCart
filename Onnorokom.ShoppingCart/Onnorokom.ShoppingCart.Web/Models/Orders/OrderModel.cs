using Autofac;
using Onnorokom.ShoppingCart.Common.DataTable;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Models.Orders
{
    public class OrderModel
    {
        private IProductOrderService _productOrderService;
        private ICartService _cartService;
        private IProductService _productService;
        private ILifetimeScope _scope;
        public int? Id { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderStatus { get; set; }
        [Required]
        [Range(1,4)]
        public int Quantity { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Flag { get; set; } = 0;

        public OrderModel()
        {

        }

        public OrderModel(IProductOrderService productOrderService, IProductService productService, ICartService cartService)
        {
            _productOrderService = productOrderService;
            _productService = productService;
            _cartService = cartService;

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _productOrderService = _scope.Resolve<IProductOrderService>();
            _productService = _scope.Resolve<IProductService>();
            _cartService = _scope.Resolve<ICartService>();
        }

        internal void Create(Guid userId, string email)
        {
            int productId = _productService.GetProductId(Name);
            var productOrder = new ProductOrder
            {
                UserId = userId,
                UserEmail = email,
                OrderDate = DateTime.Today,
                OrderStatus = "Pending",
                ProductId = productId,
                Quantity = Quantity,
            };

            _productOrderService.Create(productOrder);
            _cartService.RemoveCart(userId, productId);
        }

        internal void LoadModelData(int id)
        {
            var cart = _cartService.GetCart(id);
            var product = _productService.GetProduct(cart.ProductId);
            CategoryName = product.CategoryName;
            Price = product.Price;
            ImageName = product.ImageName;
            Name = product.Name;

        }

        internal object GetOrderDataForUser(DataTablesAjaxRequestModel tableModel,Guid userId)
        {
            var data = _productOrderService.GetProductOrdersForUser(
               userId,
               tableModel.PageIndex,
               tableModel.PageSize,
               tableModel.SearchText,
               tableModel.GetSortText(new string[] { "ProductName", "OrderDate", "DeliveryDate", "OrderStatus", "Quantity", "TotalPrice" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.ProductName,
                            record.OrderDate.ToString(),
                            record.DeliveryDate.ToString(),
                            record.OrderStatus,
                            record.Quantity.ToString(),
                            record.TotalPrice.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void RemoveOrder(int id)
        {
            _productOrderService.Remove(id);
        }

        internal void CancelProductOrder(int id)
        {
            var productOrder = _productOrderService.GetOrder(id);

            if (productOrder.OrderStatus == "Confirmed" )
                Flag = 1;
            if (productOrder.OrderStatus == "Rejected")
                Flag = 2;

        }
    }
}
