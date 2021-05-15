using Medicaly.Models;
using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["DoctorID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Post(Doctor doctor)
        {
            if (doctor != null)
            {
                if (DoctorService.login(doctor) != null)
                {
                    createSession(DoctorService.login(doctor));
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Wrong email and password", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Login", JsonRequestBehavior.AllowGet });
        }

        public void createSession(Doctor doctor)
        {
            Session["DoctorID"] = doctor.Id;
            Session["Nama"] = doctor.Nama;
            Session["Email"] = doctor.Email;
            Session["FotoProfile"] = doctor.FotoProfile;
            Session["UserType"] = "Doctor";
        }
    }
}