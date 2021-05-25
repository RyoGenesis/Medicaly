using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class ProfileService
    {
        public static ProfileViewModel getUser(string pharmacyId, string customerId, string doctorId)
        {
            ProfileViewModel profileView = new ProfileViewModel();
            if (pharmacyId != null)
            {
                profileView.pharmacies = PharmacyRepository.getPharmacyById(int.Parse(pharmacyId));
                return profileView;
            }
            else if (customerId != null)
            {
                profileView.customers = CustomerRepository.getCustomerById(int.Parse(customerId));
                profileView.Alamat = AlamatCuustomerRepository.getAlamatsCustomer(int.Parse(customerId));
                return profileView;
            } else
            {
                profileView.doctors = DoctorRepository.getDoctorById(int.Parse(doctorId));
                return profileView;
            }
        }

        public static Alamat getAlamat(string customerID, int id)
        {
            if (AlamatCuustomerRepository.getAlamat(int.Parse(customerID), id) != null)
            {
                return AlamatCuustomerRepository.getAlamat(int.Parse(customerID), id);
            }

            return null;
        }

        public static string addOrUpdateAlamat(string customerID, Alamat alamat, bool isUpdate)
        {
            if (alamat != null && customerID!= null)
            {
                alamat.CustomerID = int.Parse(customerID);


                string response = "Gagal menambahkan alamat!";
                if (!isUpdate)
                {
                    response = addAlamat(alamat);
                    if (response == null) { return response; }

                } else
                {
                    response = updateAlamat(alamat);
                    if (response == null) { return response; }
                }

                return response;

            }
  

            return "Alamat dan customerId kosong!";
        }

        private static string updateAlamat(Alamat alamat)
        {
            if (AlamatCuustomerRepository.updateAlamat(alamat.Id, alamat.LabelAlamat, alamat.NamaPenerima, alamat.KotaKecamatan, alamat.KodePost, alamat.NoHandphone))
            {
                return "Success update alamat!";
                
            }

            return null;

        }

        private static string addAlamat(Alamat alamat)
        {
            if (AlamatCuustomerRepository.addAlamat(alamat))
            {
                return "Success add alamat baru!";
            }

            return null;
        }
    }
}