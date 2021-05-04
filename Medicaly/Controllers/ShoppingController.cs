using Medicaly.Models;
using Medicaly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult Cart()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddToCart(int customerId, int quantity, int productId)
        {
            if (customerId.ToString() != null && quantity.ToString() != null && productId.ToString() != null)
            {
 
                if (CustomerService.addProductToCart(customerId, productId, quantity))
                {
                    return Json(new { success = true, message = "Added Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Failed to add the product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Failed to add the product", JsonRequestBehavior.AllowGet });
        }
    }
}