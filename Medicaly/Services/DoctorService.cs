using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class DoctorService
    {
        public static Doctor login(Doctor customer)
        {
            string email = customer.Email;
            string password = customer.Password;

            Doctor csr = DoctorRepository.getDoctorByEmailAndPassword(email, password);

            if (csr != null)
            {

                return csr;
            }

            return csr;
        }

        public static string update(string doctorId, Doctor doctor)
        {
            if (!validateUpdateEmail(int.Parse(doctorId), doctor.Email))
            {
                return "Email already registered!";
            }

            if (DoctorRepository.updateDoctor(int.Parse(doctorId), doctor.NoKTP, doctor.Nama, doctor.Email, doctor.NoHandphone, doctor.Alamat, doctor.Pengalaman, doctor.STR, doctor.SIP))
            {
                return "Success update doctor!";
            }

            return "Cannot update doctor!";
        }

        private static bool validateUpdateEmail(int id, string email)
        {
            List<Doctor> doctorList = DoctorRepository.getDoctorWherIdNot(id);
            foreach (var item in doctorList)
            {
                if (item.Email.Equals(email))
                {
                    return false;
                }
            }
            return true;
        }


        public static string updatePicture(string id, Doctor doctor, string path)
        {
            if (doctor.ImageUpload != null)
            {
                Doctor oldDoctor = DoctorRepository.getDoctorById(int.Parse(id));

                if (oldDoctor.FotoProfile != null)
                {
                    if (File.Exists(Path.Combine(path, oldDoctor.FotoProfile))) { File.Delete(Path.Combine(path, oldDoctor.FotoProfile)); }
                }
                
                string fileName = Path.GetFileNameWithoutExtension(doctor.ImageUpload.FileName);
                string extension = Path.GetExtension(doctor.ImageUpload.FileName);
                fileName = "dtr_" + oldDoctor.Nama + "_" + fileName + extension;
                doctor.FotoProfile = fileName;
                doctor.ImageUpload.SaveAs(Path.Combine(path, fileName));
            }

            if (DoctorRepository.updatePicture(int.Parse(id), doctor.FotoProfile))
            {
                return doctor.FotoProfile;
            }

            return "Cannot update profile picture!";
        }
    }
}