using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> product { get; set; }
    }
}