using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Repositories
{
    public static  class TransactionRepository
    {
        private static MedicalyDBEntities db = MedicalySingletonDB.getInstance();

        public static HeaderTransaction getHeaderTransaction()
        {
            return (from x in db.HeaderTransactions
                    orderby x.Id descending
                    select x).FirstOrDefault();
        }

        public static bool addDetail(DetailTransaction detailTransaction)
        {
            try
            {
                db.DetailTransactions.Add(detailTransaction);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool addTransaction(HeaderTransaction headerTransaction)
        {
            try
            {
                db.HeaderTransactions.Add(headerTransaction);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}