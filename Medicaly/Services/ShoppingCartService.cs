using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicaly.Factories;
using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;

namespace Medicaly.Services
{
    public static class ShoppingCartService
    {
        public static bool addProductToCart(int customerId, int productId, int quantity)
        {
            ShoppingCart shoppingCart = ShoppingCartFactory.createCart(quantity, productId, customerId);
            if (shoppingCart != null)
            {
                return ShoppingCartRepository.addProductToCart(shoppingCart);
            }

            return false;
        }

        public static ShoppingCartViewModel getCartView(int id)
        {
            List<ShoppingCart> shoppingCarts = ShoppingCartRepository.getShoppingCartByCustomerId(id);
            ShoppingCartViewModel shoppingCartView = new ShoppingCartViewModel();

            shoppingCartView.shoppingCarts = shoppingCarts;

            return shoppingCartView;
        }

        public static bool deleteCart(int cartId)
        {

            if (ShoppingCartRepository.deleteCart(cartId))
            {
                return true;
            }

            return false;
        }
    }
}