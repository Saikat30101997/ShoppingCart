using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Stocks
{
    public class CreateStockModel
    {
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string ProductName { get; set; }
    }
}
