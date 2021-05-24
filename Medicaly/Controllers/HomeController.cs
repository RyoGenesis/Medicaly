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
        // GET: Home
        public ActionResult Index()
        {
            ProductViewModel productView = ProductService.getAllProductView();
            return View(productView);
        }
    }
}