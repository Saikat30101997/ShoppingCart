using Autofac;
using AutoMapper;
using Onnorokom.ShoppingCart.Membership.Services;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories
{
    public class CreateCategoryModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private ILifetimeScope _scope;

        public CreateCategoryModel()
        {

        }

        public CreateCategoryModel(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }

        internal void Create()
        {
            var category = _mapper.Map<Category>(this);
            _categoryService.CreateCategory(category);
        }
    }
}
