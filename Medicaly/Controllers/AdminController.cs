using Medicaly.Models;
using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["AdminID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Login
        [HttpPost]
        public JsonResult Login(Admin admin)
        {
            if (admin != null)
            {
                if (AdminService.login(admin) != null)
                {
                    createSession(AdminService.login(admin));
                    return Json(new { success = true, message = "Login Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Wrong email and password", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Cannot Login", JsonRequestBehavior.AllowGet });
        }

        public void createSession(Admin admin)
        {
            Session["AdminID"] = admin.Id;
            Session["Nama"] = admin.Nama;
            Session["Email"] = admin.Email;
            Session["FotoProfile"] = admin.FotoProfile;
            Session["UserType"] = "Admin";
        }
    }
}