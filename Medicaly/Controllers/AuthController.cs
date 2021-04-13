using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                Session["UserType"] = "Customer";

                return RedirectToAction("Index", "Home");
            }

            return base.Content("<div>Username atau Password salah</div>", "text/html");
        }
    }
}
