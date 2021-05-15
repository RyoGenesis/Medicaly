using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class DoctorService
    {
        public static Doctor login(Doctor customer)
        {
            string email = customer.Email;
            string password = customer.Password;

            Doctor csr = DoctorRepository.getDoctorByEmailAndPassword(email, password);

            if (csr != null)
            {

                return csr;
            }

            return csr;
        }
    }
}