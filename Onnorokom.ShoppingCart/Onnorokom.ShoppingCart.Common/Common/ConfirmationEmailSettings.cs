using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Common.Common
{
    public class ConfirmationEmailSettings
    {
        public string host { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool useSSL { get; set; }
        public string from { get; set; }
    }
}
