using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Medicaly.Models;
using Medicaly.Services;

namespace Medicaly.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            if (Session["CustomerID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Login
        [HttpPost]
        public JsonResult Post(Customer customer)
        {
            if (customer != null)
            {
                if (CustomerService.Login(customer) != null)
                {
                    createSession(CustomerService.Login(customer));
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Wrong email and password", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Login", JsonRequestBehavior.AllowGet });
        }

        public ActionResult Register()
        {
            if (Session["CustomerID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Register
        [HttpPost]
        public JsonResult Create(Customer customer)
        {
            if (customer != null && customer.ImageUpload != null)
            {
                string path = Server.MapPath("~/AppFile/Images/Customers");
                Customer csr = CustomerService.AddCustomer(customer, path);
                if (csr != null)
                {
                    createSession(CustomerService.Login(csr));
                    return Json(new { success = true, message = "Register Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Email Already Registered", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Register", JsonRequestBehavior.AllowGet });
            
        }

        // Log Out
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }

        public void createSession(Customer customer)
        {
            Session["CustomerID"] = customer.Id;
            Session["Nama"] = customer.Nama;
            Session["Email"] = customer.Email;
            Session["FotoProfile"] = customer.FotoProfile;
            Session["UserType"] = "Customer";
        }
    }
}
