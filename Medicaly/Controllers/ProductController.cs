using Medicaly.Models;
using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail(int id)
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