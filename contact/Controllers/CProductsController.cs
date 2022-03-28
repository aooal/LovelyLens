using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using contact.Models;
using System.IO;

namespace contact.Controllers
{
    public class CProductsController : Controller
    {
        // GET: CProducts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MProducts()
        {
            DBEyeEntities2 db = new DBEyeEntities2();
            IEnumerable<t產品> datas = null;
            string keyword = Request.Form["txtKeyword"];
            string str = Request.Form["mpick"];


            if (string.IsNullOrEmpty(keyword))
            {
                datas = from p in db.t產品 select p;
            }
            else
            {
                if (str == "商品名稱")
                {
                    datas = db.t產品.Where(p => p.f產品名稱.Contains(keyword));
                }
                else if (str == "商品編號")
                {
                    datas = db.t產品.Where(p => (p.f對外產品識別ID).ToString().Contains(keyword));
                }
                else if (str == "品牌名稱")
                {
                    datas = db.t產品.Where(p => p.f品牌名稱.Contains(keyword));
                }
                else
                {
                    datas = from p in db.t產品 select p;
                }



            }

            return View(datas);
        }
        public ActionResult MProductCRUD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MProductCRUD(t產品 p)
        {
            DBEyeEntities2 db = new DBEyeEntities2();


            if (p.photo != null)
            {

                string name = Path.GetFileName(p.photo.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), name);
                p.photo.SaveAs(path);
                p.f產品圖片路徑 = name;

            }
            else
            {
                p.f產品圖片路徑 = "no-picture.jpg";
            }
            if (p.f閃光度數 != null)
            {
                db.t產品.Add(p);
            }
            else
            {
                p.f閃光度數 = "0";
            }
            db.t產品.Add(p);
            db.SaveChanges();
            return RedirectToAction("MProducts");
        }


        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == (int)id);
                if (prod != null)
                {
                    db.t產品.Remove(prod);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("MProducts");
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == (int)id);
                if (prod != null)
                    return View(prod);
            }
            return RedirectToAction("MProducts");
        }
        [HttpPost]
        public ActionResult Edit(t產品 editProduct)
        {

            DBEyeEntities2 db = new DBEyeEntities2();
            t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == editProduct.f產品ID);
            if (prod != null)
            {
                if (editProduct.photo != null)
                {
                    string name = Guid.NewGuid().ToString() + ".jpg";
                    editProduct.photo.SaveAs(Server.MapPath("../../images/") + name);

                    prod.f產品圖片路徑 = name;
                }

                prod.f成本價 = editProduct.f成本價;
                prod.f產品名稱 = editProduct.f產品名稱;
                prod.f售價 = editProduct.f售價;
                prod.f品牌名稱 = editProduct.f品牌名稱;
                prod.f庫存數量 = editProduct.f庫存數量;
                prod.f數量單位 = editProduct.f數量單位;
                prod.f有限期限 = editProduct.f有限期限;
                prod.f產品描述 = editProduct.f產品描述;
                prod.f製造批號 = editProduct.f製造批號;
                prod.f製造期限 = editProduct.f製造期限;
                prod.f近視老花度數 = editProduct.f近視老花度數;
                prod.f閃光度數 = editProduct.f閃光度數;
                prod.f閃光角度 = editProduct.f閃光角度;
                prod.f對外產品識別ID = editProduct.f對外產品識別ID;
                prod.f產品顏色 = editProduct.f產品顏色;

                db.SaveChanges();
            }
            return RedirectToAction("MProducts");
        }
        public ActionResult CopyCreate(int? id)
        {
            if (id != null)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == (int)id);
                if (prod != null)
                    return View(prod);
            }
            return RedirectToAction("MProducts");
        }
        [HttpPost]
        public ActionResult CopyCreate(t產品 theCopyCreate)
        {

            DBEyeEntities2 db = new DBEyeEntities2();


            if (theCopyCreate.photo != null)
            {

                string name = Path.GetFileName(theCopyCreate.photo.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), name);
                theCopyCreate.photo.SaveAs(path);
                theCopyCreate.f產品圖片路徑 = name;

            }

            db.t產品.Add(theCopyCreate);
            db.SaveChanges();
            return RedirectToAction("MProducts");
        }

    }
}