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

        public static HeaderTransaction getHeaderTransactionbyId(int id)
        {
            return (from x in db.HeaderTransactions
                    where x.Id == id
                    select x).FirstOrDefault();
        }


        public static List<DetailTransaction> GetDetailByCustomerId(int customerId)
        {
            return (from x in db.DetailTransactions
                    where x.HeaderTransaction.Alamat.CustomerID == customerId
                    select x).OrderByDescending(x => x.HeaderTransaction.TransactionDate).ToList();
        }

        public static List<DetailTransaction> GetDetailByApotekId(int apotekId)
        {
            return (from x in db.DetailTransactions
                    where x.Product.PharmacyId == apotekId
                    select x).ToList();
        }

        public static DetailTransaction getDetailById(int id)
        {
            return (from x in db.DetailTransactions
                    where x.Id == id 
                    select x).FirstOrDefault();
        }

        public static List<DetailTransaction> getDetailTransactionByTrId(int id)
        {
            return (from x in db.DetailTransactions
                    where x.TransactionId == id
                    select x).ToList();
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

        public static bool updateStatus(int id, int status)
        {
            try
            {
                DetailTransaction detailTransaction = getDetailById(id);

                detailTransaction.IsShipped = status;

                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static bool updateStatusShipment(int id, int status, string kurir, string trackingId)
        {
            try
            {
                DetailTransaction detailTransaction = getDetailById(id);

                detailTransaction.IsShipped = status;
                detailTransaction.Kurir = kurir;
                detailTransaction.TrackingId = trackingId;

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