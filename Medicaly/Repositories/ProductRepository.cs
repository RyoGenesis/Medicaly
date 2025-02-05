﻿using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class ProductRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static List<Product> getProductByPharmacy(int pharmacyId)
        {
            return (from x in db.Products
                    where x.PharmacyId == pharmacyId
                    select x).ToList();
        }

        public static List<Product> getAllProduct()
        {
            return (from x in db.Products
                    select x).ToList();
        }

        public static List<Product> getFiveRandomProducts()
        {
            return (from x in db.Products
                    select x).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
        }

        public static bool addProduct(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool updateProduct(Product product)
        {
            try
            {
                db.Products.AddOrUpdate(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool updateQuantity(int id, int quantity)
        {
            try
            {
                Product product = getProductById(id);

                product.Stock = quantity;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static Product getProductById(int id)
        {
            return (
                    from x in db.Products
                    where x.Id == id
                    select x
                ).FirstOrDefault();
        }

        public static List<Product> getProductsContainString(string searchQuery)
        {
            return (from x in db.Products
                    where x.Nama.Contains(searchQuery)
                    select x).ToList();
        }

        public static bool deleteProduct(int productId)
        {
            try
            {
                Product product = getProductById(productId);
                if (product != null)
                {
                    db.Products.Remove(product);
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