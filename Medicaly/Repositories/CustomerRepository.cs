using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicaly.Models;

namespace Medicaly.Repositories
{
    public class CustomerRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Customer getCustomerByEmailAndPassword(string email, string password)
        {
            return (from x in db.Customers
                    where x.Email.Equals(email) && x.Password.Equals(password)
                    select x).FirstOrDefault();
        }
    }
}