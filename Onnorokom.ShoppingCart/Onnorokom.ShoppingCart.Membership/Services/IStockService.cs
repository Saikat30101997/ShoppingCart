using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface IStockService
    {
        void Create(Stock stock);
        (IList<Stock>records, int total, int totalDisplay) Stocks(int pageIndex, 
            int pageSize, string searchText, string sortText);
    }
}
