using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class AlamatCuustomerRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();
        public static Alamat getAlamatsCustomer(int id)
        {
            return (from x in db.Alamats
                    where x.CustomerID == id
                    select x).FirstOrDefault();
        }

        public static Alamat getAlamat(int customerId, int id)
        {
            return (from x in db.Alamats
                    where x.Id == id && x.CustomerID == customerId
                    select x).FirstOrDefault();
        }

        public static Alamat getAlamat(long id)
        {
            return (from x in db.Alamats
                    where x.Id == id
                    select x).FirstOrDefault();
        }


        public static bool addAlamat(Alamat alamat)
        {
            try
            {
                db.Alamats.Add(alamat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }

        public static bool updateAlamat(long id, string labelAlamat, string namaPenerima, string kotaKecamatan, int? kodePost, string noHandphone)
        {
            try
            {

                Alamat alamat = getAlamat(id);

                alamat.LabelAlamat = labelAlamat;
                alamat.NamaPenerima = namaPenerima;
                alamat.KotaKecamatan = kotaKecamatan;
                alamat.KodePost = kodePost;
                alamat.NoHandphone = noHandphone;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false; 
            }

            return true;
        }
    }
}