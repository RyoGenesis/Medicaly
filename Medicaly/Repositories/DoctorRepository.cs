using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

        public static bool updateDoctor(int id, long? noKTP, string nama, string email, string noHandphone, string alamat, int? pengalaman, string STR, string SIP)
        {
            try
            {
                Doctor doctor = getDoctorById(id);

                doctor.NoKTP = noKTP;
                doctor.Nama = nama;
                doctor.Email = email;
                doctor.NoHandphone = noHandphone;
                doctor.Alamat = alamat;
                doctor.Pengalaman = pengalaman;
                doctor.STR = STR;
                doctor.SIP = SIP;

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

        public static List<Doctor> getDoctorWherIdNot(int id)
        {
            return (from x in db.Doctors
                    where x.Id != id
                    select x).ToList();
        }

        public static bool updatePicture(int id, string filepath)
        {
            try
            {
                Doctor doctor = getDoctorById(id);

                doctor.FotoProfile = filepath;


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