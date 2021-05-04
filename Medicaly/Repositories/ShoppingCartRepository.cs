using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicaly.Models;

namespace Medicaly.Repositories
{
    public static class ShoppingCartRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static bool addProductToCart(ShoppingCart shoppingCart)
        {
            try
            {
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static List<ShoppingCart> getShoppingCartByCustomerId(int customerId)
        {
            return (from x in db.ShoppingCarts
                    where x.CustomerId == customerId
                    select x).ToList();
        }

        public static ShoppingCart getShoppingCartById(int id)
        {
            return (from x in db.ShoppingCarts
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        public static bool deleteCart(int cartId)
        {
            try
            {
                ShoppingCart shoppingCart = getShoppingCartById(cartId);
                if (shoppingCart != null)
                {
                    db.ShoppingCarts.Remove(shoppingCart);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
