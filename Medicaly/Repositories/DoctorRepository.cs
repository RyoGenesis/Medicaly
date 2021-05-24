using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public class DoctorRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Doctor getDoctorByEmailAndPassword(string email, string password)
        {
            return (from x in db.Doctors
                    where x.Email.Equals(email) && x.Password.Equals(password)
                    select x).FirstOrDefault();
        }

        public static Doctor getDoctorById(long? id)
        {
            return (from x in db.Doctors
                    where x.Id == id
                    select x).FirstOrDefault();
        }
    }
}