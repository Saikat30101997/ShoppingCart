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
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100,MinimumLength = 5,ErrorMessage = "Product Name must be 5 to 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000,MinimumLength = 10, ErrorMessage = "Description must be 30 to 1000 characters")]
        public string Description { get; set; }
        public int CategoryId { get; set; }
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
            
            var product = _mapper.Map<Product>(this);
            product.CategoryId = CategoryId;

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
