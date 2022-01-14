﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = Onnorokom.ShoppingCart.Membership.Entities;
using BO = Onnorokom.ShoppingCart.Membership.BusinessObjects;

namespace Onnorokom.ShoppingCart.Membership.Profiles
{
    public class MembershipProfile : Profile
    {
        public MembershipProfile()
        {
            CreateMap<EO.Category, BO.Category>().ReverseMap();
        }
    }
}