using Medicaly.Models;
using Medicaly.Repositories;
using Medicaly.Services;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class PharmacyController : Controller
    {
        // GET: Pharmacy
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product
        public ActionResult Product()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Pharmacy")
            {
                ProductViewModel productView = ProductService.getProductView(int.Parse(Session["PharmacyId"].ToString()));

                return View("~/Views/Pharmacy/Products/Dashboard.cshtml", productView);
            }


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View("~/Views/Pharmacy/Auth/Login.cshtml");
        }

        public ActionResult Register()
        {
            return View("~/Views/Pharmacy/Auth/Register.cshtml");
        }

        //Login
        [HttpPost]
        public JsonResult Post(Pharmacy pharmacy)
        {
            if (pharmacy != null)
            {
                if (PharmacyService.Login(pharmacy) != null)
                {
                    createSession(PharmacyService.Login(pharmacy));
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Wrong email and password", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Login", JsonRequestBehavior.AllowGet });
        }

        //Register
        [HttpPost]
        public JsonResult Create(Pharmacy pharmacy)
        {
            if (pharmacy != null && pharmacy.ImageUpload != null)
            {
                string path = Server.MapPath("~/AppFile/Images/Pharmacies");
                Pharmacy prc = PharmacyService.AddPharmacy(pharmacy, path);
                if (prc != null)
                {
                    createSession(PharmacyService.Login(prc));
                    return Json(new { success = true, message = "Register Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Email Already Registered", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Register", JsonRequestBehavior.AllowGet });

        }




        public void createSession(Pharmacy pharmacy)
        {
            Session["PharmacyID"] = pharmacy.Id;
            Session["Nama"] = pharmacy.NamaPharmacy;
            Session["Email"] = pharmacy.EmailPharmacy;
            Session["FotoProfile"] = pharmacy.FotoPharmacy;
            Session["UserType"] = "Pharmacy";
        }
    }
}