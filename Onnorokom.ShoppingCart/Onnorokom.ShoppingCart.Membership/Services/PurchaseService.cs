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
    public class PurchaseService : IPurchaseService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Purchase purchase)
        {
            if (purchase == null)
                throw new InvalidOperationException("Purchase must be provided");

            purchase.TotalPrice = purchase.Quantity * purchase.Price;

            _shoppingCartUnitOfWork.Purchases.Add(
                _mapper.Map<Entities.Purchase>(purchase));

            _shoppingCartUnitOfWork.Save();
        }

        public (IList<Purchase> records, int total, int totalDisplay) GetPurchases(int pageIndex, 
            int pageSize, string searchText, string sortText)
        {
            var purchaseData = _shoppingCartUnitOfWork.Purchases.GetDynamic(string.IsNullOrWhiteSpace(searchText) ? null : x => x.ProductName.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var result = (from purchase in purchaseData.data
                          select _mapper.Map<Purchase>(purchase)).ToList();

            return (result, purchaseData.total, purchaseData.totalDisplay);
        }
    }
}
