using Medicaly.Models;
using Medicaly.Password;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class AdminService
    {
        public static Admin login(Admin admin)
        {
            string email = admin.Email;
            string password = admin.Password;

            Admin adm = AdminRepository.getAdminByEmail(email);

            if (adm != null)
            {
                if (Hashing.Verify(password, adm.Password)) { return adm; }
                return null;
            }

            return adm;
        }
    }
}