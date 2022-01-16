﻿using Autofac;
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
        public string Quantity { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

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
            var productOrder = new ProductOrder
            {
                UserId = userId,
                UserEmail = email,
                OrderDate = DateTime.Today,
                OrderStatus = "Pending",

            };
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
    }
}