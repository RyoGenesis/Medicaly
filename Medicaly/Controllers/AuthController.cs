using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Medicaly.Models;
using Medicaly.Repositories;

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
        public ActionResult Post(Customer customer)
        {
            string email = customer.Email;
            string password = customer.Password;

            Customer csr = CustomerRepository.getCustomerByEmailAndPassword(email, password);

            if (csr != null)
            {
                Session["CustomerID"] = csr.Id;
                Session["Nama"] = csr.Nama;
                Session["Email"] = csr.Email;
                Session["FotoProfile"] = csr.FotoProfile;
                Session["UserType"] = "Customer";

                return RedirectToAction("Index", "Home");
            }

            return base.Content("<div>Username atau Password salah</div>", "text/html");
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
                string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
                string extension = Path.GetExtension(customer.ImageUpload.FileName);
                fileName = "csr_" + customer.Nama + "_" + fileName + extension;
                customer.FotoProfile = fileName;
                customer.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images/Customers"), fileName));

                if (Repositories.CustomerRepository.addCustomer(customer))
                {
                    Session["CustomerID"] = customer.Id;
                    Session["Nama"] = customer.Nama;
                    Session["Email"] = customer.Email;
                    Session["FotoProfile"] = customer.FotoProfile;
                    Session["UserType"] = "Customer";
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
                }
                
            }

            return Json(new { success = false, message = "Cannot Successfully", JsonRequestBehavior.AllowGet });
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Auth");
        }
    }
}
