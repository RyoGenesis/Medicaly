using Medicaly.Models;
using Medicaly.Services;
using Medicaly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medicaly.Controllers
{
    public class ProductController : Controller
    {
        // GET: Udpate Product
        public ActionResult Update(int id)
        {
            if (Session["PharmacyID"] == null)
            {
                return RedirectToAction("Login", "Pharmacy");
            }

            return View(ProductService.getProductById(id));
        }

        // GET: Product
        [Route("")]
        public ActionResult Manage()
        {
            if (Session["Nama"] != null && Session["UserType"].ToString() == "Pharmacy")
            {
                ProductViewModel productView = ProductService.getProductView(int.Parse(Session["PharmacyId"].ToString()));

                return View(productView);
            }


            return RedirectToAction("Index", "Home");
        }

        // Add Product
        [HttpPost]
        public JsonResult Add(Product product)
        {
            if (product != null && product.ImageUpload != null)
            {
                string path = Server.MapPath("~/App_File/Images/Products");
                int pharmacyId = int.Parse(Session["PharmacyId"].ToString());

                if (ProductService.addProduct(pharmacyId, Session["Nama"].ToString(), product, path))
                {
                    return Json(new { success = true, message = "Added Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Add Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Update Product
        [HttpPost]
        public JsonResult Edit(Product product)
        {
            if (product != null)
            {
                string path = Server.MapPath("~/App_File/Images/Products");
                int pharmacyId = int.Parse(Session["PharmacyId"].ToString());

                if (ProductService.updateProduct(pharmacyId, Session["Nama"].ToString(), product, path))
                {
                    return Json(new { success = true, message = "Update Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Update Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // Delete Product
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (id.ToString() != null)
            {
                if (ProductService.deleteProduct(id))
                {
                    return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
                }

                return Json(new { success = false, message = "Failed Add Product", JsonRequestBehavior.AllowGet });
            }

            return Json(new { success = false, message = "Product Is Empty", JsonRequestBehavior.AllowGet });

        }

        // GET: Product Detail
        public ActionResult Detail(int id)
        {
            try
            {
                ViewBag.Message = "Your contact page.";
                ProductDetailViewModel details = ProductService.getProductDetailView(id);

                return View(details);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home"); ;
            }
        }

        //get browse result
        public ActionResult Browse(string id)
        {
            try
            {
                ViewBag.Message = "Your contact page.";
                BrowseViewModel browse = ProductService.getBrowseView(id);

                return View(browse);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home"); ;
            }
        }
    }
}