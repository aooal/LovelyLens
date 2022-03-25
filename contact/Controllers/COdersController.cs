using contact.Models;
using contact.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace contact.Controllers
{
    public class COdersController : Controller
    {
        DBEyeEntities db = new DBEyeEntities();
        // GET: COders
        [HttpGet]
        public ActionResult MOders()
        {
            var query = (from o in db.t店家
                         join p in db.t訂單
                         on o.f店家ID equals p.f店家ID
                         orderby o.f店家ID
                         select new MOrderRecord
                         {
                             f訂購日期 = p.f訂購日期,
                             f對外訂單單號 = p.f對外訂單單號,
                             f店家名稱 = o.f店家名稱,
                             f訂單總金額 = p.f訂單總金額,
                             f訂單狀態 = p.f訂單狀態,
                             f配送狀態 = p.f配送狀態
                         }).ToList();

            return View(query);
        }
        [HttpPost]
        public ActionResult MOders(string txtKeyword, string choose)
        {

            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.ViewModels.MOrderRecord> empty = new List<MOrderRecord>();
                return View(empty);
            }
            else
            {
                switch (choose)
                {

                    case "customer":
                        var query1 = (from o in db.t店家
                                      join p in db.t訂單
                                      on o.f店家ID equals p.f店家ID
                                      orderby o.f店家ID
                                      where o.f店家名稱 == txtKeyword
                                      select new MOrderRecord
                                      {
                                          f訂購日期 = p.f訂購日期,
                                          f對外訂單單號 = p.f對外訂單單號,
                                          f店家名稱 = o.f店家名稱,
                                          f訂單總金額 = p.f訂單總金額,
                                          f訂單狀態 = p.f訂單狀態,
                                          f配送狀態 = p.f配送狀態

                                      }).ToList();

                        return View(query1);

                    case "order":
                        var query2 = (from o in db.t店家
                                      join p in db.t訂單
                                      on o.f店家ID equals p.f店家ID
                                      orderby o.f店家ID
                                      where p.f對外訂單單號 == txtKeyword
                                      select new MOrderRecord
                                      {
                                          f訂購日期 = p.f訂購日期,
                                          f對外訂單單號 = p.f對外訂單單號,
                                          f店家名稱 = o.f店家名稱,
                                          f訂單總金額 = p.f訂單總金額,
                                          f訂單狀態 = p.f訂單狀態,
                                          f配送狀態 = p.f配送狀態

                                      }).ToList();

                        return View(query2);

                    default:
                        var query3 = (from o in db.t店家
                                      join p in db.t訂單
                                      on o.f店家ID equals p.f店家ID
                                      orderby o.f店家ID
                                      where o.f店家連絡電話 == txtKeyword
                                      select new MOrderRecord
                                      {
                                          f訂購日期 = p.f訂購日期,
                                          f對外訂單單號 = p.f對外訂單單號,
                                          f店家名稱 = o.f店家名稱,
                                          f訂單總金額 = p.f訂單總金額,
                                          f訂單狀態 = p.f訂單狀態,
                                          f配送狀態 = p.f配送狀態

                                      }).ToList();

                        return View(query3);
                }
            }
        }

        [HttpGet]
        public ActionResult MOderDetail(string id)
        {

            if (string.IsNullOrEmpty(id))
            {

                return RedirectToAction("MOders");
            }
            var query = (from o in db.t訂單明細
                         join p in db.t訂單
                         on o.f訂單單號ID equals p.f訂單單號ID
                         join q in db.t產品
                         on o.f產品ID equals q.f產品ID
                         join r in db.t店家
                         on q.f店家ID equals r.f店家ID
                         where p.f對外訂單單號 == id
                         select new MOderDetailRecord
                         {
                             f訂單單號ID = p.f訂單單號ID,
                             f對外訂單單號 = p.f對外訂單單號,
                             f付款狀態 = p.f付款狀態,
                             f訂單狀態 = p.f訂單狀態,
                             f訂單需求 = p.f訂單需求,
                             f對外產品識別ID = q.f對外產品識別ID,
                             f產品名稱 = q.f產品名稱,
                             f訂單總金額 = p.f訂單總金額,
                             f訂購數量 = o.f訂購數量,
                             f售價 = q.f售價,
                             f小計 = q.f售價 * o.f訂購數量,
                             f訂購日期 = p.f訂購日期,
                             f訂單備註 = p.f訂單備註,
                             f店家連絡電話 = r.f店家連絡電話,
                             f地址 = r.f地址,
                             f店家名稱 = q.f店家名稱
                         }).ToList();

            return View(query[0]);
        }



        public ActionResult MOderDetailDelete(string id)
        {
            var delId = db.t訂單.Where(m => m.f對外訂單單號 == id).FirstOrDefault();
            db.t訂單.Remove(delId);
            db.SaveChanges();
            return RedirectToAction("MOders");
        }

        [HttpGet]
        public ActionResult COders()
        {
            var query = (from o in db.t訂單
                         select o).ToList();
            return View(query);
        }
        [HttpPost]
        public ActionResult COders(string txtKeyword, string choose)
        {
            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.Models.t訂單> empty = new List<t訂單>();
                return View(empty);
            }
            else
            {
                switch (choose)
                {
                    case "customer":
                        var query = (from o in db.t店家
                                     where o.f店家名稱 == txtKeyword
                                     select o.f店家ID).FirstOrDefault();
                        var qu = from p in db.t訂單
                                 where query == p.f店家ID
                                 select p;
                        return View(qu);

                    case "order":
                        var query2 = (from o in db.t訂單
                                      where o.f對外訂單單號 == txtKeyword
                                      select o).ToList();
                        return View(query2);

                    default:
                        var query3 = (from o in db.t訂單
                                      where o.f連絡電話 == txtKeyword
                                      select o).ToList();
                        return View(query3);
                }
            }
        }
        public ActionResult COderDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var query = (from o in db.t訂單明細
                             join p in db.t訂單
                             on o.f訂單單號ID equals p.f訂單單號ID
                             join q in db.t產品
                             on o.f產品ID equals q.f產品ID
                             where p.f對外訂單單號 == id
                             select new MOderDetailRecord
                             {
                                 f對外訂單單號 = p.f對外訂單單號,
                                 f訂單狀態 = p.f訂單狀態,
                                 f訂單需求 = p.f訂單需求,
                                 f小計 = q.f售價 * o.f訂購數量,
                                 f對外產品識別ID = q.f對外產品識別ID,
                                 f產品名稱 = q.f產品名稱,
                                 f訂單總金額 = p.f訂單總金額,
                                 f訂購數量 = o.f訂購數量,
                                 f售價 = q.f售價,
                                 f付款狀態 = p.f付款狀態,
                                 f訂購日期 = p.f訂購日期

                             }).ToList();
                return View(query[0]);
            }
        }

        public ActionResult Mshipping()  //join完無資料
        {
            var query = (from o in db.t店家
                         join p in db.t訂單
                         on o.f店家ID equals p.f店家ID
                         join q in db.t產品
                         on o.f店家ID equals q.f店家ID
                         orderby o.f店家ID
                         join r in db.t訂單明細
                         on q.f產品ID equals r.f產品ID
                         select new MOrderRecord
                         {
                             f訂購日期 = p.f訂購日期,
                             f對外訂單單號 = p.f對外訂單單號,
                             f店家名稱 = o.f店家名稱,
                             f店家連絡電話 = o.f店家連絡電話,
                             f地址 = o.f地址,
                             f對外產品識別ID = q.f對外產品識別ID,
                             f產品名稱 = q.f產品名稱,
                             f訂購數量 = r.f訂購數量
                         }).ToList();
            return View(query);
        }
        public ActionResult Ccart()
        {
            return View();
        }
        public ActionResult Mexchange()
        {
            var query = (from o in db.t店家
                         join q in db.t換貨
                         on o.f店家ID equals q.f店家ID
                         orderby o.f店家ID

                         select new CexchangeRecord
                         {
                             f申請日期 = q.f申請日期,
                             f對外訂單單號 = q.f對外訂單單號,
                             f店家名稱 = o.f店家名稱,
                             f換貨申請狀態 = q.f換貨申請狀態,

                         }).ToList();

            return View(query);
        }
        [HttpPost]
        public ActionResult Mexchange(string txtKeyword, string choose)
        {
            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
                return View(empty);
            }
            else
            {
                switch (choose)
                {
                    case "customer":
                        var query = (from o in db.t店家
                                     join q in db.t換貨
                                     on o.f店家ID equals q.f店家ID
                                     orderby o.f店家ID
                                     where o.f店家名稱 == txtKeyword

                                     select new CexchangeRecord
                                     {
                                         f申請日期 = q.f申請日期,
                                         f對外訂單單號 = q.f對外訂單單號,
                                         f店家名稱 = o.f店家名稱,
                                         f換貨申請狀態 = q.f換貨申請狀態,
                                     }).ToList();

                        return View(query);

                    case "order":
                        var query2 = (from o in db.t店家
                                      join q in db.t換貨
                                      on o.f店家ID equals q.f店家ID
                                      orderby o.f店家ID
                                      where
                                     q.f對外訂單單號 == txtKeyword

                                      select new CexchangeRecord
                                      {
                                          f申請日期 = q.f申請日期,
                                          f對外訂單單號 = q.f對外訂單單號,
                                          f店家名稱 = o.f店家名稱,
                                          f換貨申請狀態 = q.f換貨申請狀態,
                                      }).ToList();

                        return View(query2);


                    default:
                        var query3 = (from o in db.t店家
                                      join q in db.t換貨
                                      on o.f店家ID equals q.f店家ID
                                      orderby o.f店家ID
                                      where
                                     o.f店家連絡電話 == txtKeyword
                                      select new CexchangeRecord
                                      {
                                          f申請日期 = q.f申請日期,
                                          f對外訂單單號 = q.f對外訂單單號,
                                          f店家名稱 = o.f店家名稱,
                                          f換貨申請狀態 = q.f換貨申請狀態,
                                      }).ToList();

                        return View(query3);
                }
            }

        }

        public ActionResult MexchangeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("CexchangeCreate");

            }
            else
            {
                var query = (from o in db.t店家
                             join q in db.t換貨
                             on o.f店家ID equals q.f店家ID
                             join p in db.t產品
                             on o.f店家ID equals p.f店家ID
                             join r in db.t換貨明細
                             on p.f產品ID equals r.f產品ID
                             orderby o.f店家ID
                             select new CexchangeDetailRecord
                             {
                                 f申請日期 = q.f申請日期,
                                 f對外訂單單號 = q.f對外訂單單號,
                                 f店家名稱 = o.f店家名稱,
                                 f店家連絡電話 = o.f店家連絡電話,
                                 f地址 = o.f地址,
                                 f對外產品識別ID = p.f對外產品識別ID,
                                 f品牌名稱 = p.f品牌名稱,
                                 f換貨數量 = r.f換貨數量,
                                 f換貨原因 = q.f換貨原因,
                                 f換貨申請狀態 = q.f換貨申請狀態
                             }).ToList();
                if (query.Count > 0)
                {
                    return View(query[0]);
                }
                else
                {
                    return View(new CexchangeDetailRecord());
                }
            }
        }//join完無資料

        [HttpGet]
        public ActionResult Cexchange()
        {

            var query = (from o in db.t店家
                         join q in db.t換貨
                         on o.f店家ID equals q.f店家ID
                         join p in db.t訂單
                         on q.f店家ID equals p.f店家ID
                         orderby o.f店家ID

                         select new CexchangeRecord
                         {
                             f申請日期 = q.f申請日期,
                             f對外訂單單號 = q.f對外訂單單號,
                             f店家名稱 = o.f店家名稱,
                             f訂單總金額 = p.f訂單總金額,
                             f換貨申請狀態 = q.f換貨申請狀態

                         }).ToList();

            return View(query);
        }
        [HttpPost]
        public ActionResult Cexchange(string txtKeyword, string choose)
        {


            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
                return View(empty);
            }

            switch (choose)
            {
                case "customer":
                    var query = (from o in db.t店家
                                 join q in db.t換貨
                                 on o.f店家ID equals q.f店家ID
                                 join p in db.t訂單
                                 on q.f店家ID equals p.f店家ID
                                 orderby o.f店家ID
                                 where o.f店家名稱 == txtKeyword
                                 select new CexchangeRecord
                                 {
                                     f申請日期 = q.f申請日期,
                                     f對外訂單單號 = q.f對外訂單單號,
                                     f店家名稱 = o.f店家名稱,
                                     f訂單總金額 = p.f訂單總金額,
                                     f換貨申請狀態 = q.f換貨申請狀態

                                 }).ToList();

                    return View(query);

                case "order":
                    var query2 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  join p in db.t訂單
                                  on q.f店家ID equals p.f店家ID
                                  orderby o.f店家ID
                                  where q.f對外訂單單號 == txtKeyword
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f對外訂單單號 = q.f對外訂單單號,
                                      f店家名稱 = o.f店家名稱,
                                      f訂單總金額 = p.f訂單總金額,
                                      f換貨申請狀態 = q.f換貨申請狀態

                                  }).ToList();
                    return View(query2);

                default:
                    var query3 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  join p in db.t訂單
                                  on q.f店家ID equals p.f店家ID
                                  orderby o.f店家ID
                                  where o.f店家連絡電話 == txtKeyword
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f對外訂單單號 = q.f對外訂單單號,
                                      f店家名稱 = o.f店家名稱,
                                      f訂單總金額 = p.f訂單總金額,
                                      f換貨申請狀態 = q.f換貨申請狀態

                                  }).ToList();

                    return View(query3);

            }
        }
        public ActionResult CexchangeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Cexchange");
            }
            else
            {
                var query = (from o in db.t店家
                             join q in db.t換貨
                             on o.f店家ID equals q.f店家ID
                             join p in db.t產品
                             on o.f店家ID equals p.f店家ID
                             join r in db.t換貨明細
                             on p.f產品ID equals r.f產品ID
                             orderby o.f店家ID
                             select new CexchangeDetailRecord
                             {
                                 f申請日期 = q.f申請日期,
                                 f對外訂單單號 = q.f對外訂單單號,
                                 f對外產品識別ID = p.f對外產品識別ID,
                                 f產品名稱 = p.f產品名稱,
                                 f換貨數量 = r.f換貨數量,
                                 f換貨申請狀態 = q.f換貨申請狀態,
                                 f訂單備註 = o.f備註,
                                 f小計 = (int)r.f換貨數量 * (int)p.f售價
                             }).ToList();
                if (query.Count > 0)
                {
                    return View(query[0]);
                }
                else
                {
                    return View(new CexchangeDetailRecord());
                }
            }
        }
        [HttpGet]
        public ActionResult CexchangeCreate()
        {

            return View();
        }  //不同資料夾無法insert

        [HttpGet]
        public ActionResult MexchangeSupplier()
        {

            var query = (from o in db.t店家
                         join q in db.t換貨
                         on o.f店家ID equals q.f店家ID
                         orderby o.f店家ID
                         select new CexchangeRecord
                         {
                             f申請日期 = q.f申請日期,
                             f對外訂單單號 = q.f對外訂單單號,
                             f店家名稱 = o.f店家名稱,
                             f換貨申請狀態 = q.f換貨申請狀態

                         }).ToList();
            return View(query);
        }
        [HttpPost]
        public ActionResult MexchangeSupplier(string txtKeyword, string choose)
        {
            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
                return View(empty);
            }
            switch (choose)
            {
                case "customer":
                    var query = (from o in db.t店家
                                 join q in db.t換貨
                                 on o.f店家ID equals q.f店家ID
                                 orderby o.f店家ID
                                 where o.f店家名稱 == txtKeyword
                                 select new CexchangeRecord
                                 {
                                     f申請日期 = q.f申請日期,
                                     f對外訂單單號 = q.f對外訂單單號,
                                     f店家名稱 = o.f店家名稱,
                                     f換貨申請狀態 = q.f換貨申請狀態

                                 }).ToList();
                    return View(query);

                case "order":
                    var query2 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  orderby o.f店家ID
                                  where q.f對外訂單單號 == txtKeyword
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f對外訂單單號 = q.f對外訂單單號,
                                      f店家名稱 = o.f店家名稱,
                                      f換貨申請狀態 = q.f換貨申請狀態

                                  }).ToList();
                    return View(query2);

                default:
                    var query3 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  orderby o.f店家ID
                                  where o.f店家連絡電話 == txtKeyword
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f對外訂單單號 = q.f對外訂單單號,
                                      f店家名稱 = o.f店家名稱,
                                      f換貨申請狀態 = q.f換貨申請狀態

                                  }).ToList();
                    return View(query3);
            }

        }
        public ActionResult MexchangeSupplierDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("MexchangeSupplier");

            }
            var query = (from o in db.t店家
                         join q in db.t換貨
                         on o.f店家ID equals q.f店家ID
                         join p in db.t產品
                         on o.f店家ID equals p.f店家ID
                         join r in db.t換貨明細
                         on p.f產品ID equals r.f產品ID
                         orderby o.f店家ID

                         select new CexchangeDetailRecord
                         {
                             f對外產品識別ID = p.f對外產品識別ID,
                             f產品名稱 = p.f產品名稱,
                             f換貨數量 = r.f換貨數量,
                             f換貨單價 = r.f換貨單價,
                             f店家名稱 = o.f店家名稱,
                             f近視老花度數 = p.f近視老花度數,
                             f閃光度數 = p.f閃光度數


                         }).ToList();
            return View(query);

        }//join完無資料
    }
}