using Medicaly.Models;
using Medicaly.Repositories;
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
                string message = CustomerService.update(Session["CustomerID"].ToString(), customer);

                Session["Nama"] = CustomerRepository.getCustomerById(int.Parse(Session["CustomerID"].ToString())).Nama;

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Customer Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Edit Customer
        [HttpPost]
        public JsonResult EditPharmacy(Pharmacy pharmacy)
        {
            if (pharmacy != null)
            {
                string message = PharmacyService.update(Session["PharmacyID"].ToString(), pharmacy);

                Session["Nama"] = PharmacyRepository.getPharmacyById(int.Parse(Session["PharmacyID"].ToString())).NamaPharmacy;

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Pharmacy Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Edit Customer
        [HttpPost]
        public JsonResult EditDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                string message = DoctorService.update(Session["DoctorID"].ToString(), doctor);

                Session["Nama"] = DoctorRepository.getDoctorById(int.Parse(Session["DoctorID"].ToString())).Nama;

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Pharmacy Is Empty", JsonRequestBehavior.AllowGet });

        }

        // GET: Alamat
        public ActionResult Alamat(string id)
        {
            if (Session["CustomerID"] != null)
            {
                
                if (id != null)
                {
                    return View(ProfileService.getAlamat(Session["CustomerID"].ToString(), int.Parse(id)));
                }
                return View();
            }


            return RedirectToAction("Index", "Home");
        }

        // Add ALamat
        [HttpPost]
        public JsonResult AddAlamat(Alamat alamat)
        {
            if (alamat != null)
            {
                string message = ProfileService.addOrUpdateAlamat(Session["CustomerID"].ToString(), alamat, false);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Edit ALamat
        [HttpPost]
        public JsonResult EditAlamat(Alamat alamat)
        {
            if (alamat != null)
            {
                string message = ProfileService.addOrUpdateAlamat(Session["CustomerID"].ToString(), alamat, true);

                return Json(new { success = true, message = message, JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public JsonResult EditPicture(Customer customer)
        {
            if (customer != null)
            {
                string path = Server.MapPath("~/App_File/Images/Customers");

                string response = CustomerService.updatePicture(Session["CustomerID"].ToString(), customer, path);

                generateResponse(response);

                return Json(new { success = true, message = generateResponse(response), JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Cannot update profile picture!", JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public JsonResult EditPicturePharmacy(Pharmacy pharmacy)
        {
            if (pharmacy != null)
            {
                string path = Server.MapPath("~/App_File/Images/Pharmacies");

                string response = PharmacyService.updatePicture(Session["PharmacyID"].ToString(), pharmacy, path);

                generateResponse(response);

                return Json(new { success = true, message = generateResponse(response), JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Cannot update profile picture!", JsonRequestBehavior.AllowGet });

        }

        [HttpPost]
        public JsonResult EditPictureDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                string path = Server.MapPath("~/App_File/Images/Doctors");

                string response = DoctorService.updatePicture(Session["DoctorID"].ToString(), doctor, path);

                generateResponse(response);

                return Json(new { success = true, message = generateResponse(response), JsonRequestBehavior.AllowGet });

            }

            return Json(new { success = false, message = "Cannot update profile picture!", JsonRequestBehavior.AllowGet });

        }

        public string generateResponse(string response)
        {
            if (response != "Cannot update profile picture!")
            {
                Session["FotoProfile"] = response;
                response = "Success update profile picture!";
            }

            return response;
        }
    }
}