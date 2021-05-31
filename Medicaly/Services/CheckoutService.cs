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
    public  static class CheckoutService
    {
        public static CheckoutViewModel getCheckoutView(string customerId)
        {
            CheckoutViewModel checkoutView = new CheckoutViewModel();
            checkoutView.shoppingCarts = ShoppingCartRepository.getShoppingCartByCustomerId(int.Parse(customerId));
            checkoutView.Alamat = AlamatCuustomerRepository.getAlamatsCustomer(int.Parse(customerId));

            return checkoutView;
        }

        public static string addTransaction(HeaderTransaction headerTransaction, string customerId, string path)
        {
            if (headerTransaction != null && customerId != null && headerTransaction.ImageUpload != null)
            {

                string name = Path.GetFileNameWithoutExtension(headerTransaction.ImageUpload.FileName);
                string extension = Path.GetExtension(headerTransaction.ImageUpload.FileName);
                string fileName = "checkout_" + headerTransaction.Id + "_" +  headerTransaction.TransactionDate + "_" + name + extension;
                headerTransaction.TransferProof = fileName;
                headerTransaction.ImageUpload.SaveAs(Path.Combine(path, fileName));
                headerTransaction.Status = "PAID";
                headerTransaction.TransactionDate = DateTime.Now.ToString();

                List<ShoppingCart> shoppingCarts = ShoppingCartRepository.getShoppingCartByCustomerId(int.Parse(customerId));

                if (TransactionRepository.addTransaction(headerTransaction))
                {
                    HeaderTransaction oldTransaction = TransactionRepository.getHeaderTransaction();

                    if (addtodetail(shoppingCarts, oldTransaction)) { return "Berhasil checkout!"; }
                }

            }


            return "Gagal melakukan checkout!";
        }

        private static bool addtodetail(List<ShoppingCart> shoppingCarts, HeaderTransaction headerTransaction)
        {
            int quantity = 0;
            foreach (ShoppingCart item in shoppingCarts)
            {
                DetailTransaction detailTransaction = new DetailTransaction();
                Product product = ProductRepository.getProductById(convertNullable(item.ProductId));
                detailTransaction.ProductId = item.ProductId;
                detailTransaction.TransactionId = headerTransaction.Id;
                detailTransaction.Quantity = item.Quantity;

                quantity = convertNullable(product.Stock) - convertNullable(item.Quantity);

                if (!ProductRepository.updateQuantity(convertNullable(item.ProductId), quantity)) { return false; }

                if (!TransactionRepository.addDetail(detailTransaction)) { return false; }

                if (!ShoppingCartRepository.deleteCart(item.Id)) { return false;  }
    
            }

            return true;
        }

        private static int convertNullable(int? number)
        {
            int newNumber = 0;
            if (number == null)
                newNumber = 0;
            else
                newNumber = number.Value;

            return newNumber;
        }
    }
}