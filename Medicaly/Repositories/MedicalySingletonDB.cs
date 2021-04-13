using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Medicaly.Models;

namespace Medicaly.Repositories
{
    public class MedicalySingletonDB
    {
        private static MedicalyDBEntities db = null;

        public static MedicalyDBEntities getInstance()
        {
            if (db == null)
            {
                db = new MedicalyDBEntities();
            }
            return db;
        }
    }
}