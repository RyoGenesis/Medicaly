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
                return profileView;
            } else
            {
                profileView.doctors = DoctorRepository.getDoctorById(int.Parse(doctorId));
                return profileView;
            }
        }


    }
}