using Medicaly.Models;
using Medicaly.Services;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductViewModel productView = ProductService.getAllProductView();
            return View(productView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Product(int id)
        {
            try
            {
                ViewBag.Message = "Your contact page.";
                Product product = ProductService.getProductById(id);

                return View(product);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home"); ;
            }
        }
    }
}