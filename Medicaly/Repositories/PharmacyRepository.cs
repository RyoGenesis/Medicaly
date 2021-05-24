﻿using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class PharmacyRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static Pharmacy getPharmacyByEmail(string emailPharmacy)
        {
            return (from x in db.Pharmacies
                    where x.EmailPharmacy.Equals(emailPharmacy)
                    select x).FirstOrDefault();
        }

        public static Pharmacy getPharmacyById(int id)
        {
            return (from x in db.Pharmacies
                    where x.Id == id
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