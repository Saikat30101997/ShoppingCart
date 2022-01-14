﻿using Onnorokom.ShoppingCart.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership.Entities
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public List<Product> Products { get; set; }
    }
}