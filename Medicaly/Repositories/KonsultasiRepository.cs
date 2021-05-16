using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static  class KonsultasiRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static List<Konsultasi> getKonsultasiBySpesialis(int spesialisId)
        {
            return (from x in db.Konsultasis
                    where x.SpesialisId == spesialisId
                    select x).ToList();
        }

        public static Konsultasi getKonsultasiById(int id)
        {
            return (from x in db.Konsultasis
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        public static bool addKonsultasi(Konsultasi konsultasi)
        {
            try
            {
                db.Konsultasis.Add(konsultasi);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool updateKonsultasi(Konsultasi konsultasi)
        {
            try
            {
                db.Entry(konsultasi).State = EntityState.Modified;
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