using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static class SpesialisRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();
        public static List<Spesiali> getAllSpesialis()
        {
            return (from x in db.Spesialis
                    select x).ToList();
        }
    }
}