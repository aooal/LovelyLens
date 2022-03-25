using contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace contact.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            DBEyeEntities db = new DBEyeEntities();

            //var Products = (from p in (new DBEyeEntities()).t產品 select p.f產品名稱).Distinct(new ProductNameComparer());
            //var Products = new DBEyeEntities().t產品.Distinct(new ProductNameComparer());
            //var nameList = (from prod in new DBEyeEntities().t產品 select prod.f產品名稱).Distinct().ToList();
            //var Products = new DBEyeEntities().t產品.Where(m => nameList.Contains(m.f產品名稱));
            var Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault());
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
                Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault());
            else
                Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault()).
                Where(p => p.f品牌名稱.Contains(keyword) || p.f產品名稱.Contains(keyword));
            return View(Products);
        }
        public ActionResult Detail(int? id)
        {
            if (id != null)
            {
                DBEyeEntities db = new DBEyeEntities();
                t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == id);
                if (prod != null)
                {
                    //return View(prod);
                    string fname = prod.f產品名稱;

                    var result = db.t產品.Where(m => m.f產品名稱 == fname);
                    return View(result);
                }
            }
            return RedirectToAction("Detail");
        }

        [HttpPost]
        public ActionResult Detail(t產品 editProduct)
        {

            DBEyeEntities db = new DBEyeEntities();
            t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == editProduct.f產品ID);

            if (prod != null)
            {

                prod.f品牌名稱 = editProduct.f品牌名稱;
                prod.f售價 = editProduct.f售價;
                prod.f對外產品識別ID = editProduct.f對外產品識別ID;
                prod.f庫存數量 = editProduct.f庫存數量;
                prod.f數量單位 = editProduct.f數量單位;
                prod.f產品描述 = editProduct.f產品描述;
                prod.f近視老花度數 = editProduct.f近視老花度數;
                prod.f閃光度數 = editProduct.f閃光度數;
                prod.f閃光角度 = editProduct.f閃光角度;
                prod.f產品顏色 = editProduct.f產品顏色;
                db.SaveChanges();
            }
            return RedirectToAction("Detail");
        }
    }
}