using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        (IList<Category>records, int total,int totalDisplay) GetCategories(int pageIndex, 
            int pageSize, string searchText, string sortText);
        bool IsCategoryAlreadyCreated(string name);
        Category GetCategory(int id);
        void Update(Category category);
    }
}
