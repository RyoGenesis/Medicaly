using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Factories
{
    public static class ShoppingCartFactory
    {
        public static ShoppingCart createCart(int quantity, int productId, int customerId)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.ProductId = productId;
            shoppingCart.CustomerId = customerId;
            shoppingCart.Quantity = quantity;
            return shoppingCart;
        }
    }
}