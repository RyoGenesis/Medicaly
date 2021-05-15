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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Doctor")
            {
                try
                {
                    KonsultasiViewModel konsultasiView = KonsultasiService.getKonsultasiView(int.Parse(Session["SpesialisId"].ToString()));
                    return View(konsultasiView);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return RedirectToAction("Index", "Home");
        }

        // View login form
        public ActionResult Login()
        {
            if (Session["DoctorID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Login
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

        // Edit Konsultasi If (answered)
        [HttpPost]
        public JsonResult EditKonsultasi(int id)
        {
            if (id.ToString() != null)
            {
                if (KonsultasiService.editKonsultasi(id))
                {
                    return Json(new { success = true, message = "Edit Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Edit Konsultasi", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Konsultasi Not Found", JsonRequestBehavior.AllowGet });

        }

        // Session
        public void createSession(Doctor doctor)
        {
            Session["DoctorID"] = doctor.Id;
            Session["Nama"] = doctor.Nama;
            Session["Email"] = doctor.Email;
            Session["SpesialisId"] = doctor.SpesialisId;
            Session["FotoProfile"] = doctor.FotoProfile;
            Session["UserType"] = "Doctor";
        }
    }
}