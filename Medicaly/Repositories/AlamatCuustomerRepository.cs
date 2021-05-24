using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class AlamatCuustomerRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();
        public static List<Alamat> getAlamatsCustomer(int id)
        {
            return (from x in db.Alamats
                    where x.CustomerID == id
                    select x).ToList();
        }
    }
}