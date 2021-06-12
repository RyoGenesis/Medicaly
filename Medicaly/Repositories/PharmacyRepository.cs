using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public static bool updatePharmacy(int id, string namaPharmacy, string emailPharmacy, string noTelephone, string alamat, string namaPIC, string emailPIC)
        {
            try
            {
                Pharmacy pharmacy = getPharmacyById(id);

                pharmacy.NamaPharmacy = namaPharmacy;
                pharmacy.EmailPharmacy = emailPharmacy;
                pharmacy.NoTelephone = noTelephone;
                pharmacy.Alamat = alamat;
                pharmacy.NamaPIC = namaPIC;
                pharmacy.EmailPIC = emailPIC;

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

        public static List<Pharmacy> getPharmacyWherIdNot(int id)
        {
            return (from x in db.Pharmacies
                    where x.Id != id
                    select x).ToList();
        }

        public static bool updatePicture(int id, string filepath)
        {
            try
            {
                Pharmacy pharmacy = getPharmacyById(id);

                pharmacy.FotoPharmacy = filepath;


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
    }
}