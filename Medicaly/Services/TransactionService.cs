using Medicaly.Models;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class TransactionService
    {
        public static string updateStatus(int id, int status, string kurir, string trackId)
        {
            if (kurir != null && trackId != null)
            {
                if (TransactionRepository.updateStatusShipment(id, status, kurir, trackId))
                {
                    return "Success update status!";
                }
            }
            else
            {
                if (TransactionRepository.updateStatus(id, status))
                {
                    return "Success update status!";
                }
            }

            return "Cannot update status!";
        }

        public static List<DetailTransaction> getTransactions(string id)
        {
            return TransactionRepository.GetDetailByApotekId(int.Parse(id));
        }
    }
}