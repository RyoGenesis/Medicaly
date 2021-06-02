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
        // GET: User Transaction 
        public ActionResult History()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Customer")
            {
                return View(TransactionService.getUserTransactions(Session["CustomerID"].ToString()));
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Detail Transaction 
        public ActionResult Detail(int id)
        {
            if (Session["Nama"] != null)
            {
                return View(TransactionService.getAllTransaction(id));
            }

            return RedirectToAction("Index", "Home");
        }

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

            return Json(new { success = false, message = "Error update!", JsonRequestBehavior.AllowGet });
        }

        // Update Status Shipment
        [HttpPost]
        public JsonResult EditStatusShipment(int id, int isShipped, string kurir, string trackingId)
        {
            if (id.ToString() != null && isShipped.ToString() != null && kurir.ToString() != null && trackingId.ToString() != null)
            {

                string message = TransactionService.updateStatus(id, isShipped, kurir, trackingId);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Error update!", JsonRequestBehavior.AllowGet });
        }
    }
}