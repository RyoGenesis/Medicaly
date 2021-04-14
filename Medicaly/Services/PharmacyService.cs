using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class PharmacyService
    {
        public static Pharmacy Login(Pharmacy pharmacy)
        {
            string emailPharmacy = pharmacy.EmailPharmacy;
            string password = pharmacy.Password;

            Pharmacy pcr = PharmacyRepository.getPharmacyByEmailAndPassword(emailPharmacy, password);

            if (pcr != null)
            {

                return pcr;
            }

            return pcr;
        }

        public static Pharmacy AddPharmacy(Pharmacy pharmacy, string path)
        {
            if (!validateEmail(pharmacy.EmailPharmacy))
            {
                return null;
            }

            string fileName = Path.GetFileNameWithoutExtension(pharmacy.ImageUpload.FileName);
            string extension = Path.GetExtension(pharmacy.ImageUpload.FileName);
            fileName = "pcr_" + pharmacy.NamaPharmacy + "_" + fileName + extension;
            pharmacy.FotoPharmacy = fileName;
            pharmacy.ImageUpload.SaveAs(Path.Combine(path, fileName));

            if (PharmacyRepository.addPharmacy(pharmacy))
            {
                return pharmacy;
            }

            return null;
        }

        private static bool validateEmail(string email)
        {
            List<Pharmacy> PharmaciesList = PharmacyRepository.getAllPharmacy();

            foreach (var item in PharmaciesList)
            {
                if (email == item.EmailPharmacy)
                {
                    return false;
                }
            }

            return true;
        }
    }
}