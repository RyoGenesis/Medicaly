using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public  static class CheckoutService
    {
        public static CheckoutViewModel getCheckoutView(string customerId)
        {
            CheckoutViewModel checkoutView = new CheckoutViewModel();
            checkoutView.shoppingCarts = ShoppingCartRepository.getShoppingCartByCustomerId(int.Parse(customerId));
            checkoutView.Alamat = AlamatCuustomerRepository.getAlamatsCustomer(int.Parse(customerId));

            return checkoutView;
        }
    }
}