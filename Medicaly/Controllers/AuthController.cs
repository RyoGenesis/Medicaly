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
            return View();
        }

        [HttpPost]
        public JsonResult Post(Customer customer)
        {
            if (customer != null)
            {
                if (AuthService.Login(customer) != null)
                {
                    createSession(AuthService.Login(customer));
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Wrong email and password", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Login", JsonRequestBehavior.AllowGet });
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Customer customer)
        {
            if (customer != null && customer.ImageUpload != null)
            {
                string path = Server.MapPath("~/AppFile/Images/Customers");
                Customer csr = AuthService.AddCustomer(customer, path);
                if (csr != null)
                {
                    createSession(AuthService.Login(csr));
                    return Json(new { success = true, message = "Register Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Email Already Registered", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Register", JsonRequestBehavior.AllowGet });
            
        }

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
