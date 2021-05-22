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
    }
}