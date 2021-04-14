using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class PharmacyRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Pharmacy getPharmacyByEmailAndPassword(string emailPharmacy, string password)
        {
            return (from x in db.Pharmacies
                    where x.EmailPharmacy.Equals(emailPharmacy) && x.Password.Equals(password)
                    select x).FirstOrDefault();
        }

        public static List<Pharmacy> getAllPharmacy()
        {
            return (from x in db.Pharmacies
                    select x).ToList();
        }

        public static bool addPharmacy(Pharmacy pharmacy)
        {
            try
            {
                db.Pharmacies.Add(pharmacy);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}