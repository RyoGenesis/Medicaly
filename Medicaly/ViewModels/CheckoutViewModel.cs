using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class CheckoutViewModel
    {
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        public Alamat Alamat { get; set; }
    }
}