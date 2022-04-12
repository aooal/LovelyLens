using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using contact.Models;
using System.IO;

namespace contact.Controllers
{
    [Authorize(Users = "1")]
    public class CDiscountController : Controller
    {
        // GET: CDiscount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MDiscountSetting()
        {
            return View();
        }
        public ActionResult MDiscountActivity()
        {
             DBEyeEntities2 db = new  DBEyeEntities2();
            IEnumerable<t優惠活動> datas = null;
            datas = from p in db.t優惠活動 select p;
            string keyword = Request.Form["txtKeyword"];
            if (string.IsNullOrEmpty(keyword))
            {
                datas = from p in db.t優惠活動 select p;
            }
            else
            {
                datas = db.t優惠活動.Where(p => p.f活動名稱.Contains(keyword));
            }
            return View(datas);
        }
        public ActionResult ActivityCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ActivityCreate(t優惠活動 activ)
        {
             DBEyeEntities2 db = new  DBEyeEntities2();
            if (activ.photo != null)
            {

                string name = Path.GetFileName(activ.photo.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), name);
                activ.photo.SaveAs(path);
                activ.f活動照片路徑 = name;

            }
            db.t優惠活動.Add(activ);
            db.SaveChanges();
            return RedirectToAction("MDiscountActivity");

        }
        public ActionResult ActivityDelete(int? id)
        {
            if (id != null)
            {
                 DBEyeEntities2 db = new  DBEyeEntities2();
                t優惠活動 prod = db.t優惠活動.FirstOrDefault(p => p.f優惠活動ID == (int)id);
                if (prod != null)
                {
                    db.t優惠活動.Remove(prod);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("MDiscountActivity");
        }
        public ActionResult ActivityDetail(int? id)
        {
            if (id != null)
            {
                 DBEyeEntities2 db = new  DBEyeEntities2();
                t優惠活動 prod = db.t優惠活動.FirstOrDefault(p => p.f優惠活動ID == id);
                if (prod != null)
                {

                    return View(prod);
                }
            }
            return RedirectToAction("MDiscountActivity");
        }
        public ActionResult ActivityEdit(int? id)
        {
            if (id != null)
            {
                 DBEyeEntities2 db = new  DBEyeEntities2();
                t優惠活動 prod = db.t優惠活動.FirstOrDefault(p => p.f優惠活動ID == (int)id);
                if (prod != null)
                    return View(prod);
            }
            return RedirectToAction("MDiscountActivity");
        }
        [HttpPost]
        public ActionResult ActivityEdit(t優惠活動 editActivity)
        {

             DBEyeEntities2 db = new  DBEyeEntities2();
            t優惠活動 prod = db.t優惠活動.FirstOrDefault(p => p.f優惠活動ID == editActivity.f優惠活動ID);
            if (prod != null)
            {
                if (editActivity.photo != null)
                {
                    string name = Guid.NewGuid().ToString() + ".jpg";
                    editActivity.photo.SaveAs(Server.MapPath("../../images/") + name);



                    prod.f活動照片路徑 = name;
                }

                prod.f活動名稱 = editActivity.f活動名稱;
                prod.f活動詳情 = editActivity.f活動詳情;
                prod.f活動起始日 = editActivity.f活動起始日;
                prod.f活動結束日 = editActivity.f活動結束日;



                db.SaveChanges();
            }
            return RedirectToAction("MDiscountActivity");
        }


    }
}