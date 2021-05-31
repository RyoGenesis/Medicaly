using Medicaly.Models;
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

        // Add transaction
        [HttpPost]
        public JsonResult Add(HeaderTransaction headerTransaction)
        {
            if (headerTransaction != null && headerTransaction.ImageUpload != null)
            {
                string path = Server.MapPath("~/App_File/Images/Transactions");
                string message = CheckoutService.addTransaction(headerTransaction, Session["CustomerID"].ToString(), path);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Failed Transaction", JsonRequestBehavior.AllowGet });

        }

    }
}