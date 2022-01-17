using AutoMapper;
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
    }
}
