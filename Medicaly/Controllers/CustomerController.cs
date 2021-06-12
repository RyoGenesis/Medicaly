using Medicaly.Services;
using Medicaly.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Support(SupportForm support)
        {
            if (support != null)
            {
                if (SupportService.sendFormToSupportEmail(support))
                {
                    return Json(new { success = true, message = "Successfully send support form", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed to send", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Failed to send", JsonRequestBehavior.AllowGet });
        }
    }
}