using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using Medicaly.Models;

namespace Medicaly.Repositories
{
    public class CustomerRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Customer getCustomerByEmail(string email)
        {
            return (from x in db.Customers
                    where x.Email.Equals(email)
                    select x).FirstOrDefault();
        }

        public static List<Customer> getAllCustomer()
        {
            return (from x in db.Customers
                    select x).ToList();
        }

        public static List<Customer> getCustomerWherIdNot(int id)
        {
            return (from x in db.Customers
                    where x.Id != id
                    select x).ToList();
        }

        public static bool addCustomer(Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool updateCustomer(int id, string nama, string email, string handphone)
        {
            try
            {
                Customer customer = getCustomerById(id);

                customer.Nama = nama;
                customer.Email = email;
                customer.NoHandphone = handphone;

                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return false;
        }



        public static Customer getCustomerById(int id)
        {
            return (from x in db.Customers
                    where x.Id == id
                    select x).FirstOrDefault();
        }
    }
}