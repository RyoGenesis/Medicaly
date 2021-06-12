using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Medicaly.Password;

namespace Medicaly.Services
{
    public static class PharmacyService
    {
        public static Pharmacy Login(Pharmacy pharmacy)
        {
            string emailPharmacy = pharmacy.EmailPharmacy;
            string password = pharmacy.Password;

            Pharmacy pcr = PharmacyRepository.getPharmacyByEmail(emailPharmacy);

            if (pcr != null) {

                if (Hashing.Verify(password, pcr.Password)) { return pcr; }

                return null; 
            }

            return null;
        }

        public static Pharmacy getPharmacy(int id)
        {
            if (PharmacyRepository.getPharmacyById(id) != null)
            {
                return PharmacyRepository.getPharmacyById(id);
            }

            return null;
        }

        public static bool AddPharmacy(Pharmacy pharmacy, string path)
        {

            if (PharmacyRepository.getPharmacyByEmail(pharmacy.EmailPharmacy) != null)
            {
                return false;
            }

            string fileName = Path.GetFileNameWithoutExtension(pharmacy.ImageUpload.FileName);
            string extension = Path.GetExtension(pharmacy.ImageUpload.FileName);
            fileName = "pcr_" + pharmacy.NamaPharmacy + "_" + fileName + extension;
            pharmacy.FotoPharmacy = fileName;
            pharmacy.ImageUpload.SaveAs(Path.Combine(path, fileName));

            //Hash Password
            pharmacy.Password = Hashing.Hash(pharmacy.Password);

            if (PharmacyRepository.addPharmacy(pharmacy))
            {
                return true;
            }

            return false;
        }

        public static string update(string pharmacyID, Pharmacy pharmacy)
        {
            if (!validateUpdateEmail(int.Parse(pharmacyID), pharmacy.EmailPharmacy))
            {
                return "Email already registered!";
            }

            if (PharmacyRepository.updatePharmacy(int.Parse(pharmacyID),  pharmacy.NamaPharmacy,  pharmacy.EmailPharmacy,  pharmacy.NoTelephone,  pharmacy.Alamat,  pharmacy.NamaPIC, pharmacy.EmailPIC))
            {
                return "Success update pharmacy!";
            }

            return "Cannot update pharmacy!";
        }

        private static bool validateUpdateEmail(int id, string email)
        {
            List<Pharmacy> pharmacyList = PharmacyRepository.getPharmacyWherIdNot(id);
            foreach (var item in pharmacyList)
            {
                if (item.EmailPharmacy.Equals(email))
                {
                    return false;
                }
            }
            return true;
        }
        public static string updatePicture(string id, Pharmacy pharmacy, string path)
        {
            if (pharmacy.ImageUpload != null)
            {
                Pharmacy oldPhamarcy = PharmacyRepository.getPharmacyById(int.Parse(id));
                if (File.Exists(Path.Combine(path, oldPhamarcy.FotoPharmacy))) { File.Delete(Path.Combine(path, oldPhamarcy.FotoPharmacy)); }


                string fileName = Path.GetFileNameWithoutExtension(pharmacy.ImageUpload.FileName);
                string extension = Path.GetExtension(pharmacy.ImageUpload.FileName);
                fileName = "pcr_" + oldPhamarcy.NamaPharmacy + "_" + fileName + extension;
                pharmacy.FotoPharmacy = fileName;
                pharmacy.ImageUpload.SaveAs(Path.Combine(path, fileName));
            }

            if (PharmacyRepository.updatePicture(int.Parse(id), pharmacy.FotoPharmacy))
            {
                return pharmacy.FotoPharmacy;
            }

            return "Cannot update profile picture!";
        }
    }
}