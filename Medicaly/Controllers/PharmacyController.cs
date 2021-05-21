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
            if (Session["PharmacyID"] == null)
            {
                return RedirectToAction("Login", "Pharmacy");
            }
            return View();
        }

        // GET: Udpate Product
        public ActionResult UpdateProduct(int id)
        {
            if (Session["PharmacyID"] == null)
            {
                return RedirectToAction("Login", "Pharmacy");
            }

            return View("~/Views/Pharmacy/Products/Update.cshtml", ProductService.getProductById(id));
        }

        // GET: Product
        [Route("")]
        public ActionResult Products()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Pharmacy")
            {
                ProductViewModel productView = ProductService.getProductView(int.Parse(Session["PharmacyId"].ToString()));

                return View("~/Views/Pharmacy/Products/Dashboard.cshtml", productView);
            }


            return RedirectToAction("Index", "Home");
        }

        // Add Product
        [HttpPost]
        public JsonResult AddProduct(Product product)
        {
            if (product != null && product.ImageUpload != null)
            {
                string path = Server.MapPath("~/App_File/Images/Products");
                int pharmacyId = int.Parse(Session["PharmacyId"].ToString());

                if (ProductService.addProduct(pharmacyId, Session["Nama"].ToString(), product, path))
                {
                    return Json(new { success = true, message = "Added Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Add Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Update Product
        [HttpPost]
        public JsonResult UpdateProductPost(Product product)
        {
            if (product != null)
            {
                string path = Server.MapPath("~/App_File/Images/Products");
                int pharmacyId = int.Parse(Session["PharmacyId"].ToString());

                if (ProductService.updateProduct(pharmacyId, Session["Nama"].ToString(), product, path))
                {
                    return Json(new { success = true, message = "Update Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Update Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Delete Product
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            if (id.ToString() != null)
            {
                if (ProductService.deleteProduct(id))
                {
                    return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Add Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        public ActionResult Login()
        {
            if (Session["PharmacyID"] != null)
            {
                return RedirectToAction("Index", "Pharmacy");
            }
            return View("~/Views/Pharmacy/Auth/Login.cshtml");
        }

        public ActionResult Register()
        {
            if (Session["PharmacyID"] != null)
            {
                return RedirectToAction("Index", "Pharmacy");
            }
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
                string path = Server.MapPath("~/App_File/Images/Pharmacies");
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