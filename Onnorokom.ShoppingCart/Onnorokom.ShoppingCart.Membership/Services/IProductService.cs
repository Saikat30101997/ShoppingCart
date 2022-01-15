using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface IProductService
    {
        void Create(Product product);
        (IList<Product>records,int total,int totalDisplay) GetProducts(int pageIndex, 
            int pageSize, string searchText, string sortText);
    }
}
