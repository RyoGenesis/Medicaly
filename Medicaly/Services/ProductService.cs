using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medicaly.Services
{
    public static class ProductService
    {
        public static ProductViewModel getProductView(int id)
        {
            List<Product> products = ProductRepository.getProductByPharmacy(id);
            ProductViewModel productView = new ProductViewModel();

            productView.product = products;

            return productView;
        }
    }
}