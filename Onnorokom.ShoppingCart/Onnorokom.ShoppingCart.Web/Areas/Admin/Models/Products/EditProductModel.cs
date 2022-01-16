using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Products
{
    public class EditProductModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public double? Price { get; set; }
        public string ImageName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CategoryName { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

        private IProductService _productService;
        private ILifetimeScope _scope;
        private IWebHostEnvironment _hostEnvironment;

        public EditProductModel()
        {

        }

        public EditProductModel(IProductService productService,IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _hostEnvironment = hostEnvironment;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _productService = _scope.Resolve<IProductService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
        }

        internal void LoadModelData(int id)
        {
            var product = _productService.GetProduct(id);

            Id = product?.Id;
            Name = product.Name;
            CategoryName = product.CategoryName;
            Price = product?.Price;
        }

        internal void Update()
        {
            var product = new Product
            {
                Id = Id.HasValue ? Id.Value : 0,
                Name = Name,
                CategoryName = CategoryName,
                ImageName = ImageName,
                Price = Price.HasValue ? Price.Value : 0
            };

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ImageFile.FileName;
            product.ImageName = DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            string path = Path.Combine(wwwRootPath + "/Image/", product.ImageName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }

            _productService.Update(product);
        }
    }
}
