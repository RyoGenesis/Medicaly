using Medicaly.Factories;
using Medicaly.Models;
using Medicaly.Password;
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
        public static Customer login(Customer customer)
        {
            string email = customer.Email;
            string password = customer.Password;

            Customer csr = CustomerRepository.getCustomerByEmail(email);

            if (csr != null)
            {
                if (Hashing.Verify(password, csr.Password)) { return csr; }
                return null;
            }

            return csr;
        }

        public static string update(string customerID, Customer customer)
        {
            if (!validateUpdateEmail(int.Parse(customerID), customer.Email))
            {
                return "Email already registered!";
            }

            string response = cekInput(customer.Nama, customer.Email, customer.NoHandphone);
            if (response != null) { return response; }


            if (CustomerRepository.updateCustomer(int.Parse(customerID), customer.Nama, customer.Email, customer.NoHandphone))
            {
                return "Success update customer!";
            }

            return "Cannot update customer!";
        }

        private static string cekInput(string nama, string email, string handphone)
        {
            if (nama == null)
            {
                return "Name cannot be empty!"; ;
            }

            if (email == null)
            {
                return "Email cannot be empty!"; ;
            }

            if (handphone == null)
            {
                return "NoHandphone cannot be empty!"; ;
            }

            return null;
        }

        private static bool validateUpdateEmail(int id, string email)
        {
            List<Customer> customerList = CustomerRepository.getCustomerWherIdNot(id);
            foreach (var item in customerList)
            {
                if (item.Email.Equals(email))
                {
                    return false;
                }
            }
            return true;
        }



        public static bool addCustomer(Customer customer, string path)
        {
            if (CustomerRepository.getCustomerByEmail(customer.Email) != null)
            {
                return false;
            }

            string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
            string extension = Path.GetExtension(customer.ImageUpload.FileName);
            fileName = "csr_" + customer.Nama + "_" + fileName + extension;
            customer.FotoProfile = fileName;
            customer.ImageUpload.SaveAs(Path.Combine(path, fileName));

            customer.Password = Hashing.Hash(customer.Password);

            if (CustomerRepository.addCustomer(customer))
            {
                return true;
            }

            return false;
        }

        public static string updatePicture(string id, Customer customer, string path)
        {
            if (customer.ImageUpload != null)
            {
                Customer oldCustomer = CustomerRepository.getCustomerById(int.Parse(id));
                if (File.Exists(Path.Combine(path, oldCustomer.FotoProfile))) { File.Delete(Path.Combine(path, oldCustomer.FotoProfile)); }


                string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
                string extension = Path.GetExtension(customer.ImageUpload.FileName);
                fileName = "csr_" + oldCustomer.Nama + "_" + fileName + extension;
                customer.FotoProfile = fileName;
                customer.ImageUpload.SaveAs(Path.Combine(path, fileName));
            }

            if (CustomerRepository.updatePicture(int.Parse(id), customer.FotoProfile))
            {
                return customer.FotoProfile;
            }

            return "Cannot update profile picture!";
        }


    }
}