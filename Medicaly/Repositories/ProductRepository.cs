using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class ProductRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static List<Product> getProductByPharmacy(int pharmacyId)
        {
            return (from x in db.Products
                    where x.PharmacyId == pharmacyId
                    select x).ToList();
        }
    }
}