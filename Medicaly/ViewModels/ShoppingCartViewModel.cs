using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
    }
}