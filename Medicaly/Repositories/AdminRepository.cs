using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class AdminRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Admin getAdminByEmail(string email)
        {
            return (from x in db.Admins
                    where x.Email.Equals(email)
                    select x).FirstOrDefault();
        }
    }
}