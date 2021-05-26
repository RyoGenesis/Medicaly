using Medicaly.Services;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class CartController : Controller
    {
        // GET: Shopping
        public ActionResult Index()
        {
            if (Session["CustomerID"] != null)
            {
                ShoppingCartViewModel shoppingCarts = CartService.getCartView(int.Parse(Session["CustomerID"].ToString()));
                return View(shoppingCarts);
                
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public JsonResult AddToCart(int customerId, int quantity, int productId)
        {
            if (customerId.ToString() != null && quantity.ToString() != null && productId.ToString() != null)
            {
 
                if (CartService.addProductToCart(customerId, productId, quantity))
                {
                    return Json(new { success = true, message = "Added Successfully", JsonRequestBehavior.AllowGet });
                }
                return Json(new { success = false, message = "Failed to add the product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Failed to add the product", JsonRequestBehavior.AllowGet });
        }

        // Delete Cart
        [HttpPost]
        public JsonResult Update(int id, int quantity)
        {
            if (id.ToString() != null && quantity.ToString() != null)
            {
               string response = CartService.updateCartQuantity(id, quantity);
               return Json(new { success = true, message = response, JsonRequestBehavior.AllowGet });
                
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Delete Cart
        [HttpPost]
        public JsonResult DeleteCart(int id)
        {
            if (id.ToString() != null)
            {
                if (CartService.deleteCart(id))
                {
                    return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Add Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }
    }
}