using Autofac;
using Onnorokom.ShoppingCart.Common.Common;
using Onnorokom.ShoppingCart.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfirmationEmailSettings>().AsSelf();
            builder.RegisterType<EmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
