using Medicaly.Models;
using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["PharmacyID"] != null)
            {
                return View(ProfileService.getUser(Session["PharmacyID"].ToString(), null, null));
            }
            else if (Session["CustomerID"] != null)
            {
                return View(ProfileService.getUser(null, Session["CustomerID"].ToString(), null));
            }
            else if (Session["DoctorID"] != null)
            {
                return View(ProfileService.getUser(null, null, Session["DoctorID"].ToString()));
            }

            return RedirectToAction("Index", "Home");
        }

        // Edit Customer
        [HttpPost]
        public JsonResult EditCustomer(Customer customer)
        {
            if (customer != null)
            {
                string message = CustomerService.update(customer);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }
    }
}