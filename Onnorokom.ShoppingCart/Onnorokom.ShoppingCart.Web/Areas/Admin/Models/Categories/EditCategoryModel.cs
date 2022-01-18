using Autofac;
using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories
{
    public class EditCategoryModel
    {
        [Required(ErrorMessage = "Please provide category name")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Category name length must be between 5 to 100")]
        [Display(Name = "Category Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        public int Id { get; set; }
        private ICategoryService _categoryService;
        private ILifetimeScope _scope;
        private IMapper _mapper;
        public EditCategoryModel()
        {

        }

        public EditCategoryModel(ICategoryService categoryService,IMapper mapper)
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

        internal void LoadCategoryData(int id)
        {
            var category = _categoryService.GetCategory(id);

            Id = category.Id;
            Name = category.Name;
        }

        internal void Edit()
        {
            var category = _mapper.Map<Category>(this);

            _categoryService.Update(category);
        }
    }
}
