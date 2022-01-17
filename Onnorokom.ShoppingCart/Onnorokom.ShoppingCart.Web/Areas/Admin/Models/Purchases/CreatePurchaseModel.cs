using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Onnorokom.ShoppingCart.Web.Areas.Admin.Models.Purchases
{
    public class CreatePurchaseModel
    {
        [Required]
        [StringLength(100,MinimumLength = 5)]
        public string SellerName { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
