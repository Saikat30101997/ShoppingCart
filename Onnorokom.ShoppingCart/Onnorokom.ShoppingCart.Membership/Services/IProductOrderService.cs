using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public interface IProductOrderService
    {
        ProductOrder GetOrder(int id);
        void Create(ProductOrder productOrder);
        (IList<ProductOrder>records,int total,int totalDisplay) GetProductOrders(int pageIndex, int pageSize,
            string searchText, string sortText);
        void AcceptProductOrder(int id, DateTime DeliveryDate);
        (IList<ProductOrder> records, int total, int totalDisplay) GetProductOrdersForUser(Guid userId, int pageIndex, int pageSize,
            string searchText, string sortText);
        void Reject(int id);
        void Remove(int id);
    }
}
