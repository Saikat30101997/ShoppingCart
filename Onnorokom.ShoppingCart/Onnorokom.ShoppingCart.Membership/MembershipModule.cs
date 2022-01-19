using Autofac;
using Onnorokom.ShoppingCart.Membership.Contexts;
using Onnorokom.ShoppingCart.Membership.Repositories;
using Onnorokom.ShoppingCart.Membership.Services;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Membership
{
    public class MembershipModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public MembershipModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ShoppingCartDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ShoppingCartDbContext>().As<IShoppingCartDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductRepository>().As<IProductRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CartRepository>().As<ICartRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductOrderRepository>().As<IProductOrderRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockRepository>().As<IStockRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<PurchaseRepository>().As<IPurchaseRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartUnitOfWork>().As<IShoppingCartUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CartService>().As<ICartService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductOrderService>().As<IProductOrderService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockService>().As<IStockService>()
              .InstancePerLifetimeScope();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductAndStockService>().As<IProductAndStockService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
