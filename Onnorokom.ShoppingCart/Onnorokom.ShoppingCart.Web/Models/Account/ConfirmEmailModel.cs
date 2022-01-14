using Autofac;

namespace Onnorokom.ShoppingCart.Web.Models.Account
{
    public class ConfirmEmailModel 
    {
        public string StatusMessage { get; set; }
        public bool IsSuccess { get; set; }

        private ILifetimeScope _scope;

        public ConfirmEmailModel()
        {
        }
    }
}