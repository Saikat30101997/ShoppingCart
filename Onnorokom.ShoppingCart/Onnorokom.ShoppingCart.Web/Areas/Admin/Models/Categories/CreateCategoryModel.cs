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
        [Required(ErrorMessage ="Please provide category name")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Category name length must be between 5 to 100")]
        [Display(Name="Category Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        private ICategoryService _categoryService;
        private IMapper _mapper;
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
            _categoryService = _scope.Resolve<ICategoryService>();
            _mapper = _scope.Resolve<IMapper>();

        }

        internal void Create()
        {
            var category = _mapper.Map<Category>(this);
            _categoryService.CreateCategory(category);
        }
    }
}
