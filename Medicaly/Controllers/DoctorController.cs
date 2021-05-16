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

        // View jawab konsultasi form
        public ActionResult Konsultasi(int id)
        {
            if (Session["DoctorID"] == null)
            {
                return RedirectToAction("Login", "Doctor");
            }
            return View(KonsultasiService.getKonsultasiById(id));
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
        public JsonResult EditKonsultasi(Konsultasi konsultasi)
        {
            if (konsultasi != null)
            {
                if (KonsultasiService.editKonsultasi(konsultasi))
                {
                    return Json(new { success = true, message = "Answer Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Answer Konsultasi", JsonRequestBehavior.AllowGet });
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