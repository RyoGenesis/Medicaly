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
    public static class CartService
    {
        public static bool addProductToCart(int customerId, int productId, int quantity)
        {
            ShoppingCart cekShoppingCart = ShoppingCartRepository.getShoppingCartByCustomerIdAndProductId(productId, customerId);

            if (cekShoppingCart != null)
            {
                cekShoppingCart.Quantity += quantity;

                return ShoppingCartRepository.updateCart(cekShoppingCart);
            } else
            {
                ShoppingCart shoppingCart = ShoppingCartFactory.createCart(quantity, productId, customerId);
                if (shoppingCart != null)
                {
                    return ShoppingCartRepository.addProductToCart(shoppingCart);
                }
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

        public static string updateCartQuantity(int cartId, int quantity)
        {

            if (ShoppingCartRepository.updateQuantity(cartId, quantity))
            {
                return "Success update quantity!";
            }

            return "Failed update quantity!";
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