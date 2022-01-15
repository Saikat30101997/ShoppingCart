using Autofac;
using AutoMapper;
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
    public class CreateProductModel
    {
        [Required]
        [StringLength(100,MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string CategoryName { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        private IProductService _productService;
        private ILifetimeScope _scope;
        private IMapper _mapper;
        private IWebHostEnvironment _hostEnvironment;
        public CreateProductModel()
        {

        }

        public CreateProductModel(IProductService productService,IMapper mapper,IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _productService = _scope.Resolve<IProductService>();
            _mapper = _scope.Resolve<IMapper>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
        }

        internal void Create()
        {
            var product = new Product
            {
                Name = Name,
                Price = Price,
                ImageName = ImageName,
                CategoryName = CategoryName,
            };
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = ImageFile.FileName;
            product.ImageName = DateTime.Now.ToString("yymmssfff") + "_" + fileName;
            string path = Path.Combine(wwwRootPath + "/Image/", product.ImageName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
            }

            _productService.Create(product);
        }
    }
}
