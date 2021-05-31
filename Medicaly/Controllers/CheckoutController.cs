using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            if (Session["CustomerID"] != null)
            {
                return View(CheckoutService.getCheckoutView(Session["CustomerID"].ToString()));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}