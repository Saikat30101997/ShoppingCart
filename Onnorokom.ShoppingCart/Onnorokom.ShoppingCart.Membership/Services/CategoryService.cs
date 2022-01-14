﻿using AutoMapper;
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
    }
}
