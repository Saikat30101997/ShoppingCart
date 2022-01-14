using Autofac;
using Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Categories;
using Onnorokom.ShoppingCart.Web.Models.Account;
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

            base.Load(builder);
        }
    }
}
