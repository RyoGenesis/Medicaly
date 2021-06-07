using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class TransactionViewModel
    {
        public IEnumerable<DetailTransaction> detailTransactions { get; set; }
        public HeaderTransaction header { get; set; }
    }
}