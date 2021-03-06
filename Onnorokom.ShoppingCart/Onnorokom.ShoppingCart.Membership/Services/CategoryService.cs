using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IShoppingCartUnitOfWork shoppingCartUnitOfWork,IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCategory(Category category)
        {
            if (category == null)
                throw new InvalidOperationException("Category must be provided");

            _shoppingCartUnitOfWork.Categories.Add(
                _mapper.Map<Entities.Category>(category));

            _shoppingCartUnitOfWork.Save();
        }

        public (IList<Category> records, int total, int totalDisplay) GetCategories(int pageIndex,
            int pageSize, string searchText, string sortText)
        {
            var categoryListData = _shoppingCartUnitOfWork.Categories.GetDynamic(string.IsNullOrWhiteSpace(searchText) ?
                null : x => x.Name.Contains(searchText), sortText, string.Empty, pageIndex, pageSize);

            var data = (from category in categoryListData.data
                        select _mapper.Map<Category>(category)).ToList();

            return (data, categoryListData.total, categoryListData.totalDisplay);
        }

        public Category GetCategory(int id)
        {
            var category = _shoppingCartUnitOfWork.Categories.GetById(id);

            return _mapper.Map<Category>(category);
        }

        public bool IsCategoryAlreadyCreated(string name)
        {
            int count = _shoppingCartUnitOfWork.Categories.GetCount(x => x.Name == name);

            if (count >= 1)
                return true;
            else
                return false;
        }

        public void Update(Category category)
        {
            var categoryEntity = _shoppingCartUnitOfWork.Categories.GetById(category.Id);

            if (categoryEntity != null)
            {
                _mapper.Map(category, categoryEntity);

                _shoppingCartUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Category is not found");
        }
    }
}
