using Medicaly.Models;
using Medicaly.Services;
using Medicaly.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class KonsultasiController : Controller
    {
        // GET: Konsultasi
        public ActionResult Index()
        {
            return View(SpesialisRepository.getAllSpesialis());
        }

        // POST: Konsultasi/Create
        [HttpPost]
        public ActionResult Create(Konsultasi konsultasi)
        {
            if (konsultasi != null)
            {
                string path = "";
                if (konsultasi.ImageUpload != null)
                {
                    path = Server.MapPath("~/App_File/Konsultasi");
                }
               
                if (KonsultasiService.addKonsultasi(konsultasi, path))
                {
                    return Json(new { success = true, message = "Asked Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed to send", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Failed to send", JsonRequestBehavior.AllowGet });
        }
    }
}
