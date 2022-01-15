using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories;
using BO = Onnorokom.ShoppingCart.Membership.BusinessObjects;

namespace Onnorokom.ShoppingCart.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<BO.Category, CreateCategoryModel>().ReverseMap();
            CreateMap<BO.Product, CreateCategoryModel>().ReverseMap();
        }
    }
}
