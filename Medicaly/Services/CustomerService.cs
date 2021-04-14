using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace Medicaly.Services
{
    public static class CustomerService
    {
        public static Customer Login(Customer customer)
        {
            string email = customer.Email;
            string password = customer.Password;

            Customer csr = CustomerRepository.getCustomerByEmailAndPassword(email, password);

            if (csr != null)
            {

                return csr;
            }

            return csr;
        }

        public static Customer AddCustomer(Customer customer, string path)
        {
            if (!validateEmail(customer.Email))
            {
                return null;
            }

            string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
            string extension = Path.GetExtension(customer.ImageUpload.FileName);
            fileName = "csr_" + customer.Nama + "_" + fileName + extension;
            customer.FotoProfile = fileName;
            customer.ImageUpload.SaveAs(Path.Combine(path, fileName));

            if (CustomerRepository.addCustomer(customer))
            {
                return customer;
            }

            return null;
        }

        private static bool validateEmail(string email)
        {
            List<Customer> customersList = CustomerRepository.getAllCustomer();

            foreach (var item in customersList)
            {
                if (email == item.Email)
                {
                    return false;
                }
            }

            return true;
        }
    }
}