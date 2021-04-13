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
        public JsonResult Post(Customer customer)
        {
            if (customer != null)
            {
                string email = customer.Email;
                string password = customer.Password;

                Customer csr = CustomerRepository.getCustomerByEmailAndPassword(email, password);

                if (csr != null)
                {
                    createSession(csr);
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
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
                if (!validateEmail(customer.Email))
                {
                    return Json(new { success = false, message = "Email Already Registered", JsonRequestBehavior.AllowGet });
                }

                string fileName = Path.GetFileNameWithoutExtension(customer.ImageUpload.FileName);
                string extension = Path.GetExtension(customer.ImageUpload.FileName);
                fileName = "csr_" + customer.Nama + "_" + fileName + extension;
                customer.FotoProfile = fileName;
                customer.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images/Customers"), fileName));

                if (Repositories.CustomerRepository.addCustomer(customer))
                {
                    createSession(customer);
                    return Json(new { success = true, message = "Register Successfully", JsonRequestBehavior.AllowGet });
                }
                
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

        private bool validateEmail(string email)
        {
            List<Customer> customersList = Repositories.CustomerRepository.getAllCustomer();

            foreach (var item in customersList)
            {
                if (email == item.Email)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
