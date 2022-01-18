using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Products;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Purchases;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks;
using BO = Onnorokom.ShoppingCart.Membership.BusinessObjects;

namespace Onnorokom.ShoppingCart.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<BO.Category, CreateCategoryModel>().ReverseMap();
            CreateMap<BO.Category, EditCategoryModel>().ReverseMap();
            CreateMap<BO.Product, CreateProductModel>().ReverseMap();
            CreateMap<BO.Product, EditProductModel>().ReverseMap();
            CreateMap<BO.Stock, CreateStockModel>().ReverseMap();
            CreateMap<BO.Purchase, CreatePurchaseModel>().ReverseMap();
            CreateMap<BO.Stock, EditStockModel>().ReverseMap();
        }
    }
}
