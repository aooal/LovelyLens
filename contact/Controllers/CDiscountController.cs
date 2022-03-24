using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using contact.Models;
using System.IO;

namespace contact.Controllers
{
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
            DBEyeEntities2 db = new DBEyeEntities2();
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
            DBEyeEntities2 db = new DBEyeEntities2();
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
    }
}