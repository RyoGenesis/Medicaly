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
    }
}