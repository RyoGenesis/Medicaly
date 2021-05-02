using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Factories
{
    public  static class ProductFactory
    {
        public static Product createProduct(int id, string nama, string deskripsi, long price, int stock, string category, string type, string productFoto, int? pharmacyId)
        {
            Product product = new Product();
            product.Id = id;
            product.Nama = nama;
            product.Deskripsi = deskripsi;
            product.Price = price;
            product.Stock = stock;
            product.Category = category;
            product.Type = type;
            product.ProductFoto = productFoto;
            product.PharmacyId = pharmacyId;

            return product;
        }
    }
}