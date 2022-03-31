using contact.Models;
using contact.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace contact.Controllers
{
    public class COdersController : Controller
    {
        DBEyeEntities2 db = new DBEyeEntities2();


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
                    case "all":
                        var query = (from o in db.t店家
                                     join p in db.t訂單
                                     on o.f店家ID equals p.f店家ID
                                     orderby o.f店家ID
                                     where o.f店家名稱.Contains(txtKeyword)
                                     select new MOrderRecord
                                     {
                                         f訂購日期 = p.f訂購日期,
                                         f對外訂單單號 = p.f對外訂單單號,
                                         f店家名稱 = o.f店家名稱,
                                         f訂單總金額 = p.f訂單總金額,
                                         f訂單狀態 = p.f訂單狀態,
                                         f配送狀態 = p.f配送狀態,
                                         f訂單備註 = p.f訂單備註

                                     }).ToList();
                        return View(query);

                    case "customer":
                        var query1 = (from o in db.t店家
                                      join p in db.t訂單
                                      on o.f店家ID equals p.f店家ID
                                      orderby o.f店家ID
                                      where o.f店家名稱.Contains(txtKeyword)
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
                                      where p.f對外訂單單號.Contains(txtKeyword)
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
                             f店家名稱 = q.f店家名稱,
                             f配送狀態 = p.f配送狀態,
                             f產品顏色 = q.f產品顏色
                         }).ToList();

            return View(query);
        }

        public ActionResult Mshipping()
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
        public ActionResult MOderDetailShipping(string id)
        {

            var cancelId = from o in db.t訂單
                           where id == o.f對外訂單單號
                           select o;
            foreach (var item in cancelId)
            {
                if (item.f配送狀態 == "未出貨" && item.f訂單狀態 != "取消")
                {
                    item.f訂單狀態 = "已出貨";
                    item.f配送狀態 = "已出貨";
                }

            }
            db.SaveChanges();
            return RedirectToAction("Mshipping");
        }

        public ActionResult MOderDetailDelete(string id)
        {

            var cancelId = from o in db.t訂單
                           where id == o.f對外訂單單號
                           select o;

            foreach (var item in cancelId)
            {
                if (item.f配送狀態 == "未出貨" && item.f訂單狀態 != "取消")
                {
                    item.f訂單狀態 = "取消";
                }

            }
            db.SaveChanges();
            return RedirectToAction("MOders");


        }
        [HttpGet]
        public ActionResult COders()
        {
            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
            var query = (from o in db.t訂單
                         where o.f店家ID == accountLogin
                         select o).ToList();
            return View(query);
        }
        public ActionResult COders(string txtKeyword, string choose)
        {
            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
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
                        var query = (from o in db.t訂單
                                     where o.f訂購日期.Contains(txtKeyword) && o.f店家ID == accountLogin
                                     select o).FirstOrDefault();

                        return View(query);

                    case "order":
                        var query2 = (from o in db.t訂單
                                      where o.f對外訂單單號.Contains(txtKeyword) && o.f店家ID == accountLogin
                                      select o).ToList();
                        return View(query2);

                    default:
                        var query3 = (from o in db.t訂單
                                      where o.f連絡電話.Contains(txtKeyword) && o.f店家ID == accountLogin
                                      select o).ToList();
                        return View(query3);
                }
            }
        }
        public ActionResult COderDetail(string id)
        {
            //var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("COders");
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
                                 f訂購日期 = p.f訂購日期,
                                 f產品顏色 = q.f產品顏色,
                                 f品牌名稱 = q.f品牌名稱,
                                 f訂單備註 = p.f訂單備註
                             }).ToList();
                return View(query);
            }
        }
        public ActionResult COderDetailDelete(string id)
        {

            var cancelId = from o in db.t訂單
                           where id == o.f對外訂單單號
                           select o;

            foreach (var item in cancelId)
            {
                if (item.f配送狀態 == "未出貨" && item.f訂單狀態 != "取消")
                {
                    item.f訂單狀態 = "取消";
                }

            }
            db.SaveChanges();
            return RedirectToAction("COders");


        }

        //[HttpGet]
        //public ActionResult COders()
        //{
        //    var query = (from o in db.t訂單
        //                 select o).ToList();
        //    return View(query);
        //}
        //[HttpPost]
        //public ActionResult COders(string txtKeyword, string choose)
        //{
        //    if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
        //    {
        //        ViewBag.data = "請輸入關鍵字";
        //        List<contact.Models.t訂單> empty = new List<t訂單>();
        //        return View(empty);
        //    }
        //    else
        //    {
        //        switch (choose)
        //        {
        //            case "all":
        //                var queryall = (from o in db.t訂單
        //                                select o).ToList();
        //                return View(queryall);
        //            case "customer":
        //                var query = (from o in db.t店家
        //                             where o.f店家名稱 == txtKeyword
        //                             select o.f店家ID).FirstOrDefault();
        //                var qu = from p in db.t訂單
        //                         where query == p.f店家ID
        //                         select p;
        //                return View(qu);

        //            case "order":
        //                var query2 = (from o in db.t訂單
        //                              where o.f對外訂單單號 == txtKeyword
        //                              select o).ToList();
        //                return View(query2);

        //            default:
        //                var query3 = (from o in db.t訂單
        //                              where o.f連絡電話 == txtKeyword
        //                              select o).ToList();
        //                return View(query3);
        //        }
        //    }
        //}
        //public ActionResult COderDetail(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return RedirectToAction("COders");
        //    }
        //    else
        //    {
        //        var query = (from o in db.t訂單明細
        //                     join p in db.t訂單
        //                     on o.f訂單單號ID equals p.f訂單單號ID
        //                     join q in db.t產品
        //                     on o.f產品ID equals q.f產品ID
        //                     where p.f對外訂單單號 == id
        //                     select new MOderDetailRecord
        //                     {
        //                         f對外訂單單號 = p.f對外訂單單號,
        //                         f訂單狀態 = p.f訂單狀態,
        //                         f訂單需求 = p.f訂單需求,
        //                         f小計 = q.f售價 * o.f訂購數量,
        //                         f對外產品識別ID = q.f對外產品識別ID,
        //                         f產品名稱 = q.f產品名稱,
        //                         f訂單總金額 = p.f訂單總金額,
        //                         f訂購數量 = o.f訂購數量,
        //                         f售價 = q.f售價,
        //                         f付款狀態 = p.f付款狀態,
        //                         f訂購日期 = p.f訂購日期,
        //                         f產品顏色 = q.f產品顏色,
        //                         f品牌名稱 = q.f品牌名稱,
        //                         f訂單備註 = p.f訂單備註
        //                     }).ToList();
        //        return View(query);
        //    }
        //}




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
                return RedirectToAction("Mexchange");

            }
            else
            {
                var query = (from o in db.t產品
                             join p in db.t訂單明細
                             on o.f產品ID equals p.f產品ID
                             select new CexchangeDetailRecord
                             {
                                 f店家名稱 = o.f店家名稱,
                                 f產品名稱 = o.f產品名稱,
                                 f訂購數量 = p.f訂購數量,
                                 f近視老花度數 = o.f近視老花度數,
                                 f閃光度數 = o.f閃光度數,
                                 f閃光角度 = o.f閃光角度,
                                 f售價 = o.f售價
                             }).ToList();
                return View(query);

            }
        }//BUG

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
                                     f換貨申請狀態 = q.f換貨申請狀態,
                                     f地址 = o.f地址,
                                     f店家連絡電話 = o.f店家連絡電話
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
                                      f換貨申請狀態 = q.f換貨申請狀態,
                                      f地址 = o.f地址,
                                      f店家連絡電話 = o.f店家連絡電話
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
                                      f換貨申請狀態 = q.f換貨申請狀態,
                                      f地址 = o.f地址,
                                      f店家連絡電話 = o.f店家連絡電話
                                  }).ToList();

                    return View(query3);

            }
        }
        [HttpGet]
        public ActionResult Cexchangecreate(string id, string pid)
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("COderDetail");
            }
            else
            {
                var query = from o in db.t訂單明細
                            join p in db.t訂單
                            on o.f訂單單號ID equals p.f訂單單號ID
                            join q in db.t產品
                            on o.f產品ID equals q.f產品ID
                            where p.f對外訂單單號 == id
                            select new MOderDetailRecord
                            {
                                f產品名稱 = q.f產品名稱,
                                f訂購數量 = o.f訂購數量,
                                f售價 = q.f售價,
                                f產品顏色 = q.f產品顏色,
                                f近視老花度數 = q.f近視老花度數,
                                f閃光度數 = q.f閃光度數,
                                f閃光角度 = q.f閃光角度,
                                f對外產品識別ID = q.f對外產品識別ID,
                                f品牌名稱 = q.f品牌名稱
                            };
                ExchangeProduct product = new ExchangeProduct();
                ExchangeProduct notFlash = new ExchangeProduct();

                product.AfterSalesService = query;
                notFlash.AfterSalesService = query;

                var detailInt = (from o in db.t訂單
                                 where o.f對外訂單單號 == id
                                 select o.f訂單單號ID).FirstOrDefault();



                var productId = (from p in db.t訂單明細
                                 where p.f訂單單號ID == detailInt
                                 select p.f產品ID).FirstOrDefault();

                var brand = db.t產品.Find(productId).f品牌名稱;
                var colors = db.t產品.Find(productId).f產品顏色;

                product.brand = brand;
                notFlash.brand = brand;
                var color = db.t產品.Where(m => m.f品牌名稱 == brand).GroupBy(c => c.f產品顏色).Select(s => s.Key);
                var flash = db.t產品.Where(m => m.f產品顏色 == colors && m.f品牌名稱 == brand).GroupBy(c => c.f閃光度數).Select(s => s.Key);
                var flashAstigmatism = db.t產品.Where(m => m.f產品顏色 == colors).GroupBy(c => c.f閃光角度).Select(s => s.Key);
                var Shortsighted = db.t產品.Where(m => m.f產品顏色 == colors && m.f品牌名稱 == brand).GroupBy(c => c.f近視老花度數).Select(s => s.Key);

                product.allColors = color;
                product.allflash = flash;
                product.allflashAstigmatism = flashAstigmatism;
                product.allShortsighted = Shortsighted;

                var toFlash = db.t產品.Where(m => m.f對外產品識別ID == pid).FirstOrDefault().f閃光度數;
                double Toflash;
                bool test = double.TryParse(toFlash, out Toflash);

                if (Toflash > 0)
                {
                    product.isPositive = true;
                    return View(product);
                }
                notFlash.allColors = color;
                notFlash.allShortsighted = Shortsighted;
                notFlash.allflash = flash;
                notFlash.allflashAstigmatism = flashAstigmatism;
                notFlash.isPositive = false;

                return View(notFlash);

            }
            return RedirectToAction("COderDetail");
        }

        [HttpPost]

        public ActionResult Cexchangecreate(string id, MOderDetailRecord cproduct, int nums)
        {

            //f產品ID f換貨數量 f換貨單價

            var product = (from o in db.t產品
                           where o.f品牌名稱 == cproduct.fabc && o.f產品顏色 == cproduct.f產品顏色 &&
                           o.f閃光度數 == cproduct.f閃光度數 && o.f閃光角度 == cproduct.f閃光角度 && o.f近視老花度數 == cproduct.f近視老花度數
                           select o
                            ).FirstOrDefault();
            int productId = product.f產品ID;

            t換貨明細 newChange = new t換貨明細();
            newChange.f產品ID = productId;
            newChange.f換貨數量 = nums;
            newChange.f換貨單價 = product.f售價;



            db.t換貨明細.Add(newChange);

            db.SaveChanges();


            return View();
        }
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
                             f閃光度數 = p.f閃光度數,
                             f成本價 = p.f成本價,
                             f售價 = p.f售價

                         }).ToList();
            if (query.Count > 0)
            {
                return View(query);
            }
            else
            {
                return RedirectToAction("MexchangeSupplier");
            }


        }
        [HttpGet]
        public ActionResult MexchangeSupplierCreate()
        {


            return View();
        }
        [HttpPost]
        public ActionResult MexchangeSupplierCreate(CexchangeDetailRecord vmodel)
        {


            return View();
        }  //寫進COOKIE 範例:購物車

    }
}