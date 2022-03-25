using contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace contact.Controllers
{
    public class CManagerController : Controller
    {
        // GET: CManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult relation()
        {
            return View();
        }
        public ActionResult supplier()
        {
            DBEyeEntities db = new DBEyeEntities();
            IEnumerable<t店家> datas = null;
            string keyword = Request.Form["txtKeyword"];
            datas = from p in db.t店家 select p;
            datas = db.t店家.Where(p => p.f身分別 == "供應商");
            if (string.IsNullOrEmpty(keyword))
            {
                datas = db.t店家.Where(p => p.f身分別 == "供應商");
            }
            else
            {
                datas = db.t店家.Where(p => p.f店家名稱.Contains(keyword));
            }
            return View(datas);
        }
        public ActionResult SupCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SupCreate(t店家 p)
        {
            DBEyeEntities db = new DBEyeEntities();

            db.t店家.Add(p);
            db.SaveChanges();
            return RedirectToAction("supplier");
        }
        public ActionResult SupDelete(int? id)
        {
            if (id != null)
            {
                DBEyeEntities db = new DBEyeEntities();
                t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == (int)id);
                if (prod != null)
                {
                    db.t店家.Remove(prod);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("supplier");
        }
        
        public ActionResult SupEdit(int? id)
        {
            if (id != null)
            {
                DBEyeEntities db = new DBEyeEntities();
                t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == (int)id);
                if (prod != null)
                    return View(prod);
            }
            return RedirectToAction("SupEdit");
        }

        [HttpPost]
        public ActionResult SupEdit(t店家 editProduct)
        {

            DBEyeEntities db = new DBEyeEntities();
            t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == editProduct.f店家ID);
            if (prod != null)
            {
                

                prod.f店家名稱 = editProduct.f店家名稱;
                prod.f店家負責人 = editProduct.f店家負責人;
                prod.f店家連絡電話 = editProduct.f店家連絡電話;
                prod.f電子信箱 = editProduct.f電子信箱;
                prod.f銀行帳號 = editProduct.f銀行帳號;
                prod.f備註 = editProduct.f備註;
                
                db.SaveChanges();
            }
            return RedirectToAction("SupEdit");
        }
        public ActionResult customer()
        {
            DBEyeEntities db = new DBEyeEntities();
            IEnumerable<t店家> datas = null;
            string keyword = Request.Form["txtKeyword"];
            string str = Request.Form["mpick"];

            datas = from p in db.t店家 select p;
            datas = db.t店家.Where(p => p.f身分別 == "經銷商");

            if (string.IsNullOrEmpty(keyword))
            {
                datas = db.t店家.Where(p => p.f身分別 == "經銷商");
            }
            else
            {
                if (str == "店家名稱")
                {
                    datas = db.t店家.Where(p => p.f店家名稱.Contains(keyword));
                }
                else if (str == "電話")
                {
                    datas = db.t店家.Where(p => p.f身分別 == "經銷商" && (p.f店家連絡電話).ToString().Contains(keyword));
                }
                else if (str == "往來狀態")
                {
                    datas = db.t店家.Where(p => p.f往來狀態.Contains(keyword));
                }
                else
                {
                    datas = db.t店家.Where(p => p.f身分別 == "經銷商");
                }
            }
            return View(datas);
        }
        public ActionResult CusDelete(int? id)
        {
            if (id != null)
            {
                DBEyeEntities db = new DBEyeEntities();
                t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == (int)id);
                if (prod != null)
                {
                    db.t店家.Remove(prod);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("customer");
        }
        public ActionResult CusEdit(int? id)
        {
            if (id != null)
            {
                DBEyeEntities db = new DBEyeEntities();
                t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == (int)id);
                if (prod != null)
                    return View(prod);
            }
            return RedirectToAction("customer");
        }

        [HttpPost]
        public ActionResult CusEdit(t店家 editProduct)
        {

            DBEyeEntities db = new DBEyeEntities();
            t店家 prod = db.t店家.FirstOrDefault(p => p.f店家ID == editProduct.f店家ID);
            if (prod != null)
            {

                prod.f地址 = editProduct.f地址;
                prod.f密碼 = editProduct.f密碼;
                prod.f往來狀態 = editProduct.f往來狀態;
                prod.f註冊日期 = editProduct.f註冊日期;
                prod.f店家ID = editProduct.f店家ID;
                prod.f店家名稱 = editProduct.f店家名稱;
                prod.f店家負責人 = editProduct.f店家負責人;
                prod.f店家連絡電話 = editProduct.f店家連絡電話;
                prod.f電子信箱 = editProduct.f電子信箱;
                prod.f銀行帳號 = editProduct.f銀行帳號;
                prod.f備註 = editProduct.f備註;

                db.SaveChanges();
            }
            return RedirectToAction("customer");
        }
    }
}