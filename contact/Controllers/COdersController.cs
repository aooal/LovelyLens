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
        }  //OK
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
                                      where p.f訂購日期.Contains(txtKeyword)
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
        }  //OK

        [HttpGet]

        public ActionResult MOderDetail(string dId)
        {

            if (string.IsNullOrEmpty(dId))
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
                         where p.f對外訂單單號 == dId

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
                             f產品顏色 = q.f產品顏色,
                             f近視老花度數 = q.f近視老花度數,
                             f閃光角度 = q.f閃光角度,

                             f閃光度數 = q.f閃光度數,
                             f可換貨 = false
                         }).ToList();

            foreach (var item in query)
            {

                var checkOut = db.t產品.Where(
                    m => m.f產品名稱 == item.f產品名稱
                    && m.f近視老花度數 == item.f近視老花度數
                    && m.f閃光角度 == item.f閃光角度
                    && m.f閃光度數 == item.f閃光度數

                    );

                var a = checkOut.Count();

                if (a == 0)
                {
                    item.f可換貨 = false;
                }
                else
                {
                    int count = 0;
                    foreach (var checkOutResult in checkOut)
                    {
                        count += checkOutResult.f庫存數量;

                    }
                    if (count >= item.f訂購數量)
                    {
                        item.f可換貨 = true;
                    }
                }
            }


            return View(query);
        }  //OK
        public ActionResult MOderUpdate(string dId)
        {
            if (string.IsNullOrEmpty(dId))
            {
                return RedirectToAction("MOders");
            }
            var orderCause = from o in db.t訂單
                             where o.f對外訂單單號 == dId
                             select new MOderDetailRecord
                             {
                                 f對外訂單單號 = o.f對外訂單單號,
                                 f訂單備註 = o.f訂單備註
                             };

            return View(orderCause);
        }   //OK
        [HttpPost]
        public ActionResult MOderUpdate(string dId, string f訂單備註)
        {
            var theCause = from o in db.t訂單
                           where o.f對外訂單單號 == dId
                           select o;
            foreach (var cause in theCause)
            {
                cause.f訂單備註 = f訂單備註;
            }
            db.SaveChanges();
            return RedirectToAction("MOders");
        }  //OK
        public ActionResult MOderDetailDelete(string dId)
        {

            var cancelId = from o in db.t訂單
                           where dId == o.f對外訂單單號
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


        }  //OK

        public ActionResult Mshipping()
        {
            var exchangeToShipping = (from o in db.t換貨
                                      join p in db.t換貨明細
                                      on o.f售後服務申請對外Id equals p.f售後服務申請對外Id
                                      join a in db.t售後服務申請
                                      on o.f售後服務申請對外Id equals a.f售後服務申請對外Id
                                      join q in db.t產品
                                      on p.f產品ID equals q.f產品ID
                                      join r in db.t店家
                                      on q.f店家ID equals r.f店家ID
                                      where q.f庫存數量 > p.f換貨數量
                                      select new MOrderRecord
                                      {
                                          f申請日期 = o.f申請日期,
                                          f售後服務申請對外Id = o.f售後服務申請對外Id,
                                          f產品顏色 = a.f產品顏色,
                                          f產品名稱 = a.f產品名稱,
                                          f店家名稱 = q.f店家名稱,
                                          f換貨申請狀態 = o.f換貨申請狀態,
                                          f換貨數量 = p.f換貨數量,
                                          f對外產品識別ID = q.f對外產品識別ID,
                                          f店家連絡電話 = r.f店家連絡電話,
                                          f地址 = r.f地址,
                                          f近視老花度數 = q.f近視老花度數,
                                          f閃光角度 = q.f閃光角度,
                                          f可換貨 = false
                                      }).ToList();
            return View(exchangeToShipping);
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
        }  //OK




        [HttpGet]
        public ActionResult COders()
        {
            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
            var query = (from o in db.t訂單
                         where o.f店家ID == accountLogin
                         select o).ToList();
            return View(query);
        }  //OK
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


                    case "order":
                        var query = (from o in db.t訂單
                                     where o.f對外訂單單號.Contains(txtKeyword)
                                     && o.f店家ID == accountLogin
                                     select o).ToList();
                        return View(query);

                    default:
                        var query2 = (from o in db.t訂單
                                      where o.f訂購日期.Contains(txtKeyword)
                                      && o.f店家ID == accountLogin
                                      select o).ToList();
                        return View(query2);

                }
            }
        }  //OK
        public ActionResult COderDetail(string id)
        {
            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);

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
                             where p.f對外訂單單號 == id && p.f店家ID == accountLogin
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
        }  //OK
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

        }  //OK


        public ActionResult Mexchange()
        {
            var query = (from o in db.t換貨
                         join p in db.t換貨明細
                         on o.f售後服務申請對外Id equals p.f售後服務申請對外Id
                         join q in db.t產品
                         on p.f產品ID equals q.f產品ID
                         join r in db.t店家
                         on q.f店家ID equals r.f店家ID
                         select new CexchangeRecord
                         {
                             f申請日期 = o.f申請日期,
                             f售後服務申請對外Id = o.f售後服務申請對外Id,
                             f店家名稱 = q.f店家名稱,
                             f換貨申請狀態 = o.f換貨申請狀態,
                             f對外產品識別ID = q.f對外產品識別ID,
                             f產品名稱 = q.f產品名稱,
                             f換貨數量 = p.f換貨數量,
                             f店家連絡電話 = r.f店家連絡電話,
                             f地址 = r.f地址
                         }).ToList();

            return View(query);
        }  //OK
        [HttpPost]
        public ActionResult Mexchange(string txtKeyword, string choose)
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
                                 join p in db.t換貨
                                 on q.f店家ID equals p.f店家ID
                                 orderby o.f店家ID
                                 where o.f店家名稱.Contains(txtKeyword)
                                 select new CexchangeRecord
                                 {
                                     f申請日期 = q.f申請日期,
                                     f售後服務申請對外Id = q.f售後服務申請對外Id,
                                     f店家名稱 = o.f店家名稱,
                                     f換貨申請狀態 = q.f換貨申請狀態

                                 }).ToList();

                    return View(query);

                case "order":
                    var query2 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  join p in db.t換貨
                                  on q.f店家ID equals p.f店家ID
                                  orderby o.f店家ID
                                  where q.f售後服務申請對外Id.Contains(txtKeyword)
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f售後服務申請對外Id = q.f售後服務申請對外Id,
                                      f店家名稱 = o.f店家名稱,
                                      f換貨申請狀態 = q.f換貨申請狀態

                                  }).ToList();

                    return View(query2);

                default:
                    var query3 = (from o in db.t店家
                                  join q in db.t換貨
                                  on o.f店家ID equals q.f店家ID
                                  join p in db.t換貨
                                  on q.f店家ID equals p.f店家ID
                                  orderby o.f店家ID
                                  where q.f申請日期.Contains(txtKeyword)
                                  select new CexchangeRecord
                                  {
                                      f申請日期 = q.f申請日期,
                                      f售後服務申請對外Id = q.f售後服務申請對外Id,
                                      f店家名稱 = o.f店家名稱,
                                      f換貨申請狀態 = q.f換貨申請狀態,


                                  }).ToList();

                    return View(query3);

            }

        }  //OK

        public ActionResult MexchangeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Mexchange");
            }

            var query = (from o in db.t售後服務申請
                         join p in db.t換貨
                         on o.f售後服務申請對外Id equals p.f售後服務申請對外Id
                         join q in db.t產品
                         on p.f店家ID equals q.f店家ID
                         where o.f售後服務申請對外Id == id
                         select new CexchangeRecord
                         {

                             f申請日期 = p.f申請日期,
                             f售後服務申請對外Id = o.f售後服務申請對外Id,
                             f店家名稱 = q.f店家名稱,
                             f換貨申請狀態 = p.f換貨申請狀態,
                             f對外產品識別ID = q.f對外產品識別ID,
                             f產品名稱 = q.f產品名稱,
                             f換貨數量 = o.f數量,
                             //f店家連絡電話 = r.f店家連絡電話,
                             //f地址 = r.f地址,
                             f換貨原因 = o.f換貨原因,
                             f換貨備註 = o.f換貨備註
                         }).FirstOrDefault();

            //foreach(var item in query)
            //{

            //    var checkOut = db.t產品.Where(
            //        m => m.f產品名稱 == item.f產品名稱
            //        && m.f近視老花度數 == item.f近視老花度數
            //        && m.f閃光角度 == item.f閃光角度
            //        && m.f閃光度數 == item.f閃光度數

            //        );

            //    var a = checkOut.Count();

            //    if (a == 0)
            //    {
            //        item.f可換貨 = false;
            //    }
            //    else
            //    {
            //        int count = 0;
            //        foreach (var checkOutResult in checkOut)
            //        {
            //            count += checkOutResult.f庫存數量;

            //        }
            //        if (count >= item.f換貨數量)
            //        {
            //            item.f可換貨 = true;
            //        }
            //    }
            //}


            return View(query);

        }//判斷庫存

        [HttpGet]
        public ActionResult Cexchange()
        {
            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
            var query = (from o in db.t換貨
                         join p in db.t店家
                         on o.f店家ID equals p.f店家ID
                         where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin
                         select new CexchangeRecord
                         {
                             f申請日期 = o.f申請日期,
                             f售後服務申請對外Id = o.f售後服務申請對外Id,
                             f換貨申請狀態 = o.f換貨申請狀態

                         }).ToList();
            return View(query);
        }//OK
        [HttpPost]
        public ActionResult Cexchange(string txtKeyword, string choose)
        {
            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);

            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
            {
                ViewBag.data = "請輸入關鍵字";
                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
                return View(empty);
            }

            switch (choose)
            {
                case "date":
                    var query = (from o in db.t換貨
                                 join p in db.t店家
                                 on o.f店家ID equals p.f店家ID
                                 where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin && o.f申請日期.Contains(txtKeyword)
                                 select new CexchangeRecord
                                 {
                                     f申請日期 = o.f申請日期,
                                     f售後服務申請對外Id = o.f售後服務申請對外Id,
                                     f換貨申請狀態 = o.f換貨申請狀態
                                 }).ToList();

                    return View(query);

                default:
                    var query2 = (from o in db.t換貨
                                  join p in db.t店家
                                  on o.f店家ID equals p.f店家ID
                                  where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin && o.f對外訂單單號.Contains(txtKeyword)

                                  select new CexchangeRecord
                                  {
                                      f申請日期 = o.f申請日期,
                                      f售後服務申請對外Id = o.f售後服務申請對外Id,
                                      f換貨申請狀態 = o.f換貨申請狀態
                                  }).ToList();

                    return View(query2);


            }
        }//OK
        public ActionResult CexchangeDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Cexchange");
            }
            var query = (from o in db.t換貨
                         join p in db.t換貨明細
                         on o.f換貨單號ID equals p.f換貨單號ID
                         join q in db.t售後服務申請
                         on o.f售後服務申請對外Id equals q.f售後服務申請對外Id
                         where p.f售後服務申請對外Id == id
                         select new CexchangeRecord
                         {
                             f申請日期 = o.f申請日期,
                             f售後服務申請對外Id = o.f售後服務申請對外Id,
                             f換貨申請狀態 = o.f換貨申請狀態,
                             f產品名稱 = q.f產品名稱,
                             f近視老花度數 = q.f近視老花度數,
                             f閃光度數 = q.f閃光度數,
                             f閃光角度 = q.f閃光角度,
                             f數量 = q.f數量,
                             f換貨原因 = o.f換貨原因,
                             f換貨備註 = o.f換貨備註
                         }).FirstOrDefault();

            return View(query);
        }//OK
        [HttpGet]
        public ActionResult Cexchangecreate(string id, string pid) //OK pid產品對外ID 
        {

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("COderDetail");
            }
            else
            {
                var query = from o in db.t訂單明細
                            join b in db.t訂單
                            on o.f訂單單號ID equals b.f訂單單號ID
                            join q in db.t產品
                            on o.f產品ID equals q.f產品ID
                            where b.f對外訂單單號 == id
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

                var productId = (from o in db.t產品
                                 where o.f對外產品識別ID == pid
                                 select o.f產品ID).FirstOrDefault();

                var detailInt = (from o in db.t訂單
                                 where o.f對外訂單單號 == id
                                 select o.f訂單單號ID).FirstOrDefault();

                var brand = db.t產品.Find(productId).f品牌名稱;
                var colors = db.t產品.Find(productId).f產品顏色;
                var pName = db.t產品.Find(productId).f產品名稱;

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

        public ActionResult Cexchangecreate(string exchangeOutId, string id, string pid, int nums, MOderDetailRecord cproduct, string f換貨原因, string f換貨備註, decimal? price, string f產品名稱, string f申請日期, int f店家ID, string f售後服務申請對外Id) //OK 要存經銷商cookie userid
        {
            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
            var detailInt = (from o in db.t訂單
                             where o.f對外訂單單號 == id
                             select o.f訂單單號ID).FirstOrDefault();

            var dFid = (from o in db.t訂單明細
                        where o.f訂單單號ID == detailInt
                        select o.f訂單明細ID).FirstOrDefault();

            var productId = (from o in db.t產品
                             where o.f對外產品識別ID == pid
                             select o.f產品ID).FirstOrDefault();
            var pName = (from o in db.t產品
                         where o.f產品ID == productId
                         select o.f產品名稱).FirstOrDefault();
            var pColor = (from o in db.t產品
                          where o.f產品ID == productId
                          select o.f產品顏色).FirstOrDefault();
            var pShort = (from o in db.t產品
                          where o.f產品ID == productId
                          select o.f近視老花度數).FirstOrDefault();
            var pFlash = (from o in db.t產品
                          where o.f產品ID == productId
                          select o.f閃光度數).FirstOrDefault();
            var pAstigmatism = (from o in db.t產品
                                where o.f產品ID == productId
                                select o.f閃光角度).FirstOrDefault();


            var exChangeId = (from o in db.t產品
                              where o.f產品名稱 == pName && o.f產品顏色 == pColor && o.f近視老花度數 == pShort && o.f閃光度數 == pFlash && o.f閃光角度 == pAstigmatism
                              select o.f產品ID).FirstOrDefault();

            var cName = (from o in db.t產品
                         where o.f產品ID == productId
                         select o.f店家名稱).FirstOrDefault();


            var cTel = (from o in db.t店家
                        where o.f店家名稱 == cName
                        select o.f店家連絡電話).FirstOrDefault();


            var cId = (from o in db.t店家
                       where o.f店家名稱 == cName
                       select o.f店家ID).FirstOrDefault();


            var changedId = (from o in db.t換貨
                             where o.f售後服務申請對外Id == f售後服務申請對外Id
                             select o.f換貨單號ID).FirstOrDefault();

            t售後服務申請 after = new t售後服務申請();
            after.f售後服務申請對外Id = exchangeOutId;
            after.f數量 = nums;
            after.f產品名稱 = f產品名稱;
            after.f產品顏色 = cproduct.f產品顏色;
            after.f近視老花度數 = cproduct.f近視老花度數;
            after.f閃光度數 = cproduct.f閃光度數;
            after.f閃光角度 = cproduct.f閃光角度;
            after.f換貨單價 = price;
            after.f換貨原因 = f換貨原因;
            after.f換貨備註 = f換貨備註;

            db.t售後服務申請.Add(after);

            t換貨明細 newChangeDetail = new t換貨明細();
            newChangeDetail.f產品ID = exChangeId;
            newChangeDetail.f要換的產品ID = productId;
            newChangeDetail.f換貨數量 = nums;
            newChangeDetail.f要換的數量 = nums;
            newChangeDetail.f換貨單價 = price;
            newChangeDetail.f要換的產品單價 = price;
            newChangeDetail.f售後服務申請對外Id = exchangeOutId;
            newChangeDetail.f換貨單號ID = changedId;

            db.t換貨明細.Add(newChangeDetail);


            f申請日期 = DateTime.Now.ToString("yyyy-MM-dd");
            t換貨 exchanging = new t換貨();
            exchanging.f申請日期 = f申請日期;
            exchanging.f換貨原因 = f換貨原因;
            exchanging.f換貨備註 = f換貨備註;
            exchanging.f換貨申請人 = cName;
            exchanging.f連絡電話 = cTel;
            exchanging.f店家ID = cId;
            exchanging.f店家ID = accoutLogin;//
            exchanging.f對外訂單單號 = id;
            exchanging.f訂單明細ID = dFid;
            exchanging.f換貨申請狀態 = "未出貨";
            exchanging.f售後服務申請對外Id = exchangeOutId;


            db.t換貨.Add(exchanging);

            db.SaveChanges();

            return RedirectToAction("COderDetail");
        }

        public ActionResult CexchangeDelete(string id)
        {
            var cancelId = from o in db.t換貨
                           where id == o.f售後服務申請對外Id
                           select o;
            foreach (var item in cancelId)
            {
                if (item.f換貨申請狀態 == "未出貨" && item.f換貨申請狀態 != "取消")
                {
                    item.f換貨申請狀態 = "取消";
                }

            }
            db.SaveChanges();
            return RedirectToAction("Cexchange");

        } //OK





        public ActionResult MexchangeSupplierCreate(int id)
        {

            var supplier = from s in db.t產品
                           where s.f店家ID == id
                           select s;




            return View(supplier);
        }

        public ActionResult MexchangingSupplierCreate(int id)
        {
            var sName = db.t產品.Find(id).f店家名稱;  //供應商名稱
            var sProduct = db.t產品.Where(m => m.f店家名稱 == sName).GroupBy(n => n.f產品名稱).Select(n => n.Key);

            var sShort = db.t產品.Where(m => sProduct.Contains(m.f產品名稱)).GroupBy(n => n.f近視老花度數).Select(n => n.Key);
            var sFlash = db.t產品.Where(m => sProduct.Contains(m.f產品名稱)).GroupBy(n => n.f閃光度數).Select(n => n.Key);
            var sFlashA = db.t產品.Where(m => sProduct.Contains(m.f產品名稱)).GroupBy(n => n.f閃光角度).Select(n => n.Key);

            var sProductTo = sProduct.ToList();
            var sShortTo = sShort.ToList();
            var sFlashTo = sFlash.ToList();
            var sFlashATo = sFlashA.ToList();

            ViewBag.sProduct = sProductTo;
            ViewBag.sShort = sShortTo;
            ViewBag.sFlashA = sFlashATo;
            ViewBag.sFlash = sFlashTo;

            var sExchange = from s in db.t店家
                            where s.f身分別 == "供應商"
                            select s.f店家名稱;

            //供應商名稱 商品 規格 顏色

            return View();
        }
        [HttpPost]
        public ActionResult MexchangingSupplierCreate(CexchangeDetailRecord vmodel)
        {


            return View();
        }  //寫進COOKIE 範例:購物車

    }
}