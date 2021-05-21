using Medicaly.Factories;
using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class ProductService
    {
        public static ProductViewModel getAllProductView()
        {
            List<Product> products = ProductRepository.getAllProduct();
            ProductViewModel productView = new ProductViewModel();

            productView.product = products;

            return productView;
        }

        public static ProductViewModel getProductView(int id)
        {
            List<Product> products = ProductRepository.getProductByPharmacy(id);
            ProductViewModel productView = new ProductViewModel();

            productView.product = products;

            return productView;
        }

        public static Product getProductById(int id)
        {
            if (id.ToString() != null)
            {
                if (ProductRepository.getProductById(id) != null)
                {
                    return ProductRepository.getProductById(id);
                }
            }

            return null;
        }

        public static bool addProduct(int pharmacyId, string pharmacyName, Product product, string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
            string extension = Path.GetExtension(product.ImageUpload.FileName);
            fileName = pharmacyName + "_" + product.Nama + "_" + fileName + extension;
            product.ProductFoto = fileName;
            product.ImageUpload.SaveAs(Path.Combine(path, fileName));
            product.PharmacyId = pharmacyId;

            if (ProductRepository.addProduct(product))
            {
                return true;
            }

            return false;
        }

        public static bool updateProduct(int pharmacyId, string pharmacyName, Product product, string path)
        {
            if (product.ImageUpload != null)
            {
                if (File.Exists(Path.Combine(path, product.ProductFoto))) { File.Delete(Path.Combine(path, product.ProductFoto)); }

                string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                string extension = Path.GetExtension(product.ImageUpload.FileName);
                fileName = pharmacyName + "_" + product.Nama + "_" + fileName + extension;
                product.ProductFoto = fileName;
                product.ImageUpload.SaveAs(Path.Combine(path, fileName));
                product.PharmacyId = pharmacyId;
            }

            if (ProductRepository.updateProduct(product))
            {
                return true;
            }

            return false;
        }

        public static bool deleteProduct(int productId)
        {

            if (ProductRepository.deleteProduct(productId))
            {
                return true;
            }

            return false;
        }
    }
}