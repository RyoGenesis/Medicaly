using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class ProfileViewModel
    {
        public Pharmacy pharmacies { get; set; }
        public Doctor doctors { get; set; }
        public Customer customers { get; set; }

        public Alamat Alamat { get; set; }
    }
}