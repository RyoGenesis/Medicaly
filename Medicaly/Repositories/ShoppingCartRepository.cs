﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static ShoppingCart getShoppingCartByCustomerIdAndProductId(int productId, int customerId)
        {
            return (from x in db.ShoppingCarts
                    where x.CustomerId == customerId && x.ProductId == productId
                    select x).FirstOrDefault();

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

        public static bool updateQuantity(int id, int quantity)
        {
            try
            {
               ShoppingCart shoppingCart =  getShoppingCartById(id);

                shoppingCart.Quantity = quantity;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool updateCart(ShoppingCart shoppingCart)
        {
            try
            {
                db.Entry(shoppingCart).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
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
