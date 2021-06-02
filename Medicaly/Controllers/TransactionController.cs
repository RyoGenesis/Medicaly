using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Manage()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Pharmacy")
            {
                return View(TransactionService.getTransactions(Session["PharmacyId"].ToString()));
            }

            return RedirectToAction("Index", "Home");
        }

        // Update Status
        [HttpPost]
        public JsonResult EditStatus(int id, int isShipped)
        {
            if (id.ToString() != null && isShipped.ToString() != null)
            {

                string message = TransactionService.updateStatus(id, isShipped, null, null);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });
        }
    }
}