using Medicaly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.ViewModels
{
    public class BrowseViewModel
    {
        public IEnumerable<Product> product { get; set; }
        public string searchInput { get; set; }
    }
}