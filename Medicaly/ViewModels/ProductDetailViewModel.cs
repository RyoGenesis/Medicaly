using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class ProductDetailViewModel
    {
        public IEnumerable<Product> products { get; set; }
        public Product selectedProduct { get; set; }
    }
}