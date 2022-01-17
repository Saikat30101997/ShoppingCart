using Autofac;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Orders;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Products;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks;
using Onnorokom.ShoppingCart.Web.Models.Account;
using Onnorokom.ShoppingCart.Web.Models.Carts;
using Onnorokom.ShoppingCart.Web.Models.Orders;
using Onnorokom.ShoppingCart.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<ConfirmEmailModel>().AsSelf();
            builder.RegisterType<ForgotPasswordModel>().AsSelf();
            builder.RegisterType<ResetPasswordModel>().AsSelf();
            builder.RegisterType<CreateCategoryModel>().AsSelf();
            builder.RegisterType<CategoryListModel>().AsSelf();
            builder.RegisterType<CreateProductModel>().AsSelf();
            builder.RegisterType<ProductListModel>().AsSelf();
            builder.RegisterType<ProductModel>().AsSelf();
            builder.RegisterType<EditProductModel>().AsSelf();
            builder.RegisterType<CartModel>().AsSelf();
            builder.RegisterType<OrderModel>().AsSelf();
            builder.RegisterType<OrderListModel>().AsSelf();
            builder.RegisterType<CreateStockModel>().AsSelf();

            base.Load(builder);
        }
    }
}
