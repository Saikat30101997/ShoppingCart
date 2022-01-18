using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface IPurchaseService
    {
        void Create(Purchase purchase);
        (IList<Purchase>records,int total, int totalDisplay) GetPurchases(int pageIndex, 
            int pageSize, string searchText, string sortText);
    }
}
