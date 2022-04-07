//using contact.Models;
//using contact.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.UI.WebControls;

//namespace contact.Controllers
//{
//    public class COdersController : Controller
//    {
//        DBEyeEntities2 db = new DBEyeEntities2();


//        [HttpGet]
//        public ActionResult MOders()
//        {
//            var query = (from o in db.t店家
//                         join p in db.t訂單
//                         on o.f店家ID equals p.f店家ID
//                         orderby o.f店家ID
//                         select new MOrderRecord
//                         {
//                             f訂購日期 = p.f訂購日期,
//                             f對外訂單單號 = p.f對外訂單單號,
//                             f店家名稱 = o.f店家名稱,
//                             f訂單總金額 = p.f訂單總金額,
//                             f訂單狀態 = p.f訂單狀態,
//                             f配送狀態 = p.f配送狀態,
//                             f訂單備註 = p.f訂單備註
//                         }).ToList();

//            return View(query);
//        }  //OK
//        public ActionResult MOders(string txtKeyword, string choose)
//        {

//            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
//            {
//                ViewBag.data = "請輸入關鍵字";
//                List<contact.ViewModels.MOrderRecord> empty = new List<MOrderRecord>();
//                return View(empty);
//            }
//            else
//            {
//                switch (choose)
//                {


//                    case "customer":
//                        var query1 = (from o in db.t店家
//                                      join p in db.t訂單
//                                      on o.f店家ID equals p.f店家ID
//                                      orderby o.f店家ID
//                                      where o.f店家名稱.Contains(txtKeyword)
//                                      select new MOrderRecord
//                                      {
//                                          f訂購日期 = p.f訂購日期,
//                                          f對外訂單單號 = p.f對外訂單單號,
//                                          f店家名稱 = o.f店家名稱,
//                                          f訂單總金額 = p.f訂單總金額,
//                                          f訂單狀態 = p.f訂單狀態,
//                                          f配送狀態 = p.f配送狀態,
//                                          f訂單備註 = p.f訂單備註
//                                      }).ToList();

//                        return View(query1);

//                    case "order":
//                        var query2 = (from o in db.t店家
//                                      join p in db.t訂單
//                                      on o.f店家ID equals p.f店家ID
//                                      orderby o.f店家ID
//                                      where p.f對外訂單單號.Contains(txtKeyword)
//                                      select new MOrderRecord
//                                      {
//                                          f訂購日期 = p.f訂購日期,
//                                          f對外訂單單號 = p.f對外訂單單號,
//                                          f店家名稱 = o.f店家名稱,
//                                          f訂單總金額 = p.f訂單總金額,
//                                          f訂單狀態 = p.f訂單狀態,
//                                          f配送狀態 = p.f配送狀態,
//                                          f訂單備註 = p.f訂單備註

//                                      }).ToList();

//                        return View(query2);

//                    default:
//                        var query3 = (from o in db.t店家
//                                      join p in db.t訂單
//                                      on o.f店家ID equals p.f店家ID
//                                      orderby o.f店家ID
//                                      where o.f店家連絡電話.Contains(txtKeyword)
//                                      select new MOrderRecord
//                                      {
//                                          f訂購日期 = p.f訂購日期,
//                                          f對外訂單單號 = p.f對外訂單單號,
//                                          f店家名稱 = o.f店家名稱,
//                                          f訂單總金額 = p.f訂單總金額,
//                                          f訂單狀態 = p.f訂單狀態,
//                                          f配送狀態 = p.f配送狀態,
//                                          f訂單備註 = p.f訂單備註

//                                      }).ToList();

//                        return View(query3);
//                }
//            }
//        }  //OK

//        [HttpGet]

//        public ActionResult MOderDetail(string dId)
//        {

//            if (string.IsNullOrEmpty(dId))
//            {

//                return RedirectToAction("MOders");
//            }
//            var query = (from o in db.t訂單明細
//                         join p in db.t訂單
//                         on o.f訂單單號ID equals p.f訂單單號ID
//                         join q in db.t產品
//                         on o.f產品ID equals q.f產品ID
//                         join r in db.t店家
//                         on q.f店家ID equals r.f店家ID
//                         where p.f對外訂單單號 == dId

//                         select new MOderDetailRecord
//                         {
//                             f訂單單號ID = p.f訂單單號ID,
//                             f對外訂單單號 = p.f對外訂單單號,
//                             f付款狀態 = p.f付款狀態,
//                             f訂單狀態 = p.f訂單狀態,
//                             f訂單需求 = p.f訂單需求,
//                             f對外產品識別ID = q.f對外產品識別ID,
//                             f產品名稱 = q.f產品名稱,
//                             f訂單總金額 = p.f訂單總金額,
//                             f訂購數量 = o.f訂購數量,
//                             f售價 = q.f售價,
//                             f小計 = q.f售價 * o.f訂購數量,
//                             f訂購日期 = p.f訂購日期,
//                             f訂單備註 = p.f訂單備註,
//                             f店家連絡電話 = r.f店家連絡電話,
//                             f地址 = r.f地址,
//                             f店家名稱 = q.f店家名稱,
//                             f配送狀態 = p.f配送狀態,
//                             f產品顏色 = q.f產品顏色,
//                             f近視老花度數 = q.f近視老花度數,
//                             f閃光角度 = q.f閃光角度,

//                             f閃光度數 = q.f閃光度數,
//                             f可換貨 = false
//                         }).ToList();

//            foreach (var item in query)
//            {

//                var checkOut = db.t產品.Where(
//                    m => m.f產品名稱 == item.f產品名稱
//                    && m.f近視老花度數 == item.f近視老花度數
//                    && m.f閃光角度 == item.f閃光角度
//                    && m.f閃光度數 == item.f閃光度數

//                    );

//                var a = checkOut.Count();

//                if (a == 0)
//                {
//                    item.f可換貨 = false;
//                }
//                else
//                {
//                    int? count = 0;
//                    foreach (var checkOutResult in checkOut)
//                    {
//                        count += checkOutResult.f庫存數量;

//                    }
//                    if (count >= item.f訂購數量)
//                    {
//                        item.f可換貨 = true;
//                    }
//                }
//            }


//            return View(query);
//        }  //OK
//        public ActionResult MOderUpdate(string dId)
//        {
//            if (string.IsNullOrEmpty(dId))
//            {
//                return RedirectToAction("MOders");
//            }
//            var orderCause = from o in db.t訂單
//                             where o.f對外訂單單號 == dId
//                             select new MOderDetailRecord
//                             {
//                                 f對外訂單單號 = o.f對外訂單單號,
//                                 f訂單備註 = o.f訂單備註
//                             };

//            return View(orderCause);
//        }   //OK
//        [HttpPost]
//        public ActionResult MOderUpdate(string dId, string f訂單備註)
//        {
//            var theCause = from o in db.t訂單
//                           where o.f對外訂單單號 == dId
//                           select o;
//            foreach (var cause in theCause)
//            {
//                cause.f訂單備註 = f訂單備註;
//            }
//            db.SaveChanges();
//            return RedirectToAction("MOders");
//        }  //OK
//        public ActionResult MOderDetailDelete(string dId)
//        {

//            var cancelId = from o in db.t訂單
//                           where dId == o.f對外訂單單號
//                           select o;

//            foreach (var item in cancelId)
//            {
//                if (item.f配送狀態 == "未出貨" && item.f訂單狀態 != "取消")
//                {
//                    item.f訂單狀態 = "取消";
//                }

//            }
//            db.SaveChanges();
//            return RedirectToAction("MOders");


//        }  //OK

//        public ActionResult Mshipping()
//        { //判斷庫存
//            var query = new List<MOrderRecord>();

//            var orderList = (from o in db.t店家
//                             join p in db.t訂單
//                             on o.f店家ID equals p.f店家ID
//                             select new MOrderRecord
//                             {
//                                 f訂單單號ID = p.f訂單單號ID,
//                                 f對外訂單單號 = p.f對外訂單單號,
//                                 f訂購日期 = p.f訂購日期,
//                                 f店家名稱 = o.f店家名稱,
//                                 //f訂購數量 = r.f訂購數量,
//                                 f店家連絡電話 = o.f店家連絡電話,
//                                 f地址 = o.f地址,
//                                 f配送狀態 = p.f配送狀態,
//                                 f電子信箱 = o.f電子信箱
//                                 //f產品名稱 = q.f產品名稱,
//                                 //f對外產品識別ID = q.f對外產品識別ID,
//                                 //f可換貨 = false
//                             }).ToList();
//            foreach (var orderItem in orderList)
//            {
//                // 訂單中所有項目
//                var orderProducts = (from r in db.t訂單明細
//                                     join q in db.t產品
//                                     on r.f產品ID equals q.f產品ID
//                                     where r.f訂單單號ID == orderItem.f訂單單號ID
//                                     select new MOrderRecord
//                                     {
//                                         f訂購日期 = orderItem.f訂購日期,
//                                         f對外訂單單號 = orderItem.f對外訂單單號,
//                                         f店家名稱 = orderItem.f店家名稱,
//                                         f訂購數量 = r.f訂購數量,
//                                         f店家連絡電話 = orderItem.f店家連絡電話,
//                                         f地址 = orderItem.f地址,
//                                         f配送狀態 = orderItem.f配送狀態,
//                                         f產品名稱 = q.f產品名稱,
//                                         f對外產品識別ID = q.f對外產品識別ID,
//                                         f庫存數量 = q.f庫存數量,
//                                         f電子信箱 = orderItem.f電子信箱
//                                     }).ToList();

//                if (orderProducts.Where(product => product.f訂購數量 > product.f庫存數量).Count() == 0)
//                {
//                    query.AddRange(orderProducts);
//                }
//            }

//            var exchangeToShipping = (from o in db.t換貨
//                                      join p in db.t換貨明細
//                                      on o.f售後服務申請對外Id equals p.f售後服務申請對外Id
//                                      join a in db.t售後服務申請
//                                      on o.f售後服務申請對外Id equals a.f售後服務申請對外Id
//                                      join q in db.t產品
//                                      on p.f產品ID equals q.f產品ID
//                                      join r in db.t店家
//                                      on q.f店家ID equals r.f店家ID
//                                      where q.f庫存數量 > p.f換貨數量
//                                      select new MOrderRecord
//                                      {
//                                          f申請日期 = o.f申請日期,
//                                          f售後服務申請對外Id = o.f售後服務申請對外Id,
//                                          f產品顏色 = a.f產品顏色,
//                                          f產品名稱 = a.f產品名稱,
//                                          f店家名稱 = q.f店家名稱,
//                                          f換貨申請狀態 = o.f換貨申請狀態,
//                                          f換貨數量 = p.f換貨數量,
//                                          f對外產品識別ID = q.f對外產品識別ID,
//                                          f店家連絡電話 = r.f店家連絡電話,
//                                          f地址 = r.f地址,
//                                          f近視老花度數 = q.f近視老花度數,
//                                          f閃光角度 = q.f閃光角度,
//                                          f電子信箱 = r.f電子信箱,
//                                          f可換貨 = false
//                                      }).ToList();
//            ShippingViewModels exchangeToShip = new ShippingViewModels();

//            exchangeToShip.query = query;
//            exchangeToShip.exchangeToShipping = exchangeToShipping;



//            return View(exchangeToShip);
//        }
//        public ActionResult MOderDetailShipping(string id)
//        {
//            var order = (from o in db.t訂單
//                         where id == o.f對外訂單單號
//                         select o).FirstOrDefault();

//            if (order != null)
//            {
//                var orderProducts = db.t訂單明細.Where(detail => detail.f訂單單號ID == order.f訂單單號ID).ToList();
//                orderProducts.ForEach(orderProduct => {
//                    var product = db.t產品.Where(p => p.f產品ID == orderProduct.f產品ID).FirstOrDefault();
//                    if (product != null)
//                    {
//                        product.f庫存數量 = (int)(product.f庫存數量 - orderProduct.f訂購數量);
//                    }
//                });

//                if (order.f配送狀態 == "未出貨" && order.f訂單狀態 != "取消")
//                {
//                    order.f訂單狀態 = "已出貨";
//                    order.f配送狀態 = "已出貨";
//                }
//                db.SaveChanges();
//            }
//            return RedirectToAction("Mshipping");
//        }  //OK
//        public ActionResult MexchangeShipping(string id)
//        {
//            var order = (from o in db.t訂單
//                         where id == o.f對外訂單單號
//                         select o).FirstOrDefault();
//            var exchange = (from o in db.t換貨
//                            where id == o.f售後服務申請對外Id
//                            select o).FirstOrDefault();
//            if (exchange != null)
//            {
//                var exchangeProducts = db.t換貨明細.Where(detail => detail.f換貨單號ID == exchange.f換貨單號ID).ToList();
//                exchangeProducts.ForEach(exchangeProduct =>
//                {
//                    var product = db.t產品.Where(p => p.f產品ID == exchangeProduct.f產品ID).FirstOrDefault();
//                    if (product != null)
//                    {
//                        product.f庫存數量 = (int)(product.f庫存數量 - exchangeProduct.f換貨數量);
//                    }
//                });

//                if (exchange.f換貨申請狀態 == "未出貨")
//                {

//                    exchange.f換貨申請狀態 = "已出貨";
//                }
//                db.SaveChanges();
//            }


//            return RedirectToAction("Mshipping");
//        }  //OK

//        [HttpGet]
//        public ActionResult COders()
//        {
//            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
//            var query = (from o in db.t訂單
//                         where o.f店家ID == accountLogin
//                         select o).ToList();
//            return View(query);
//        }  //OK
//        public ActionResult COders(string txtKeyword, string choose)
//        {
//            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
//            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
//            {

//                ViewBag.data = "請輸入關鍵字";
//                List<contact.Models.t訂單> empty = new List<t訂單>();
//                return View(empty);
//            }
//            else
//            {

//                switch (choose)
//                {


//                    case "order":
//                        var query = (from o in db.t訂單
//                                     where o.f對外訂單單號.Contains(txtKeyword)
//                                     && o.f店家ID == accountLogin
//                                     select o).ToList();
//                        return View(query);

//                    default:
//                        var query2 = (from o in db.t訂單
//                                      where o.f訂購日期.Contains(txtKeyword)
//                                      && o.f店家ID == accountLogin
//                                      select o).ToList();
//                        return View(query2);

//                }
//            }
//        }  //OK
//        public ActionResult COderDetail(string id)
//        {
//            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);

//            if (string.IsNullOrEmpty(id))
//            {
//                return RedirectToAction("COders");
//            }
//            else
//            {
//                var query = (from o in db.t訂單明細
//                             join p in db.t訂單
//                             on o.f訂單單號ID equals p.f訂單單號ID
//                             join q in db.t產品
//                             on o.f產品ID equals q.f產品ID
//                             where p.f對外訂單單號 == id && p.f店家ID == accountLogin
//                             select new MOderDetailRecord
//                             {
//                                 f對外訂單單號 = p.f對外訂單單號,
//                                 f訂單狀態 = p.f訂單狀態,
//                                 f訂單需求 = p.f訂單需求,
//                                 f小計 = q.f售價 * o.f訂購數量,
//                                 f對外產品識別ID = q.f對外產品識別ID,
//                                 f產品名稱 = q.f產品名稱,
//                                 f訂單總金額 = p.f訂單總金額,
//                                 f訂購數量 = o.f訂購數量,
//                                 f售價 = q.f售價,
//                                 f付款狀態 = p.f付款狀態,
//                                 f訂購日期 = p.f訂購日期,
//                                 f產品顏色 = q.f產品顏色,
//                                 f品牌名稱 = q.f品牌名稱,
//                                 f訂單備註 = p.f訂單備註,
//                                 f近視老花度數 = q.f近視老花度數,
//                                 f閃光度數 = q.f閃光度數,
//                                 f閃光角度 = q.f閃光角度
//                             }).ToList();
//                return View(query);
//            }
//        }  //OK
//        public ActionResult COderDetailDelete(string id)
//        {

//            var cancelId = from o in db.t訂單
//                           where id == o.f對外訂單單號
//                           select o;

//            foreach (var item in cancelId)
//            {
//                if (item.f配送狀態 == "未出貨" && item.f訂單狀態 != "取消")
//                {
//                    item.f訂單狀態 = "取消";
//                }

//            }

//            db.SaveChanges();
//            return RedirectToAction("COders");

//        }  //OK


//        public ActionResult Mexchange()
//        {
//            var query = (from o in db.t換貨
//                         join p in db.t換貨明細
//                         on o.f售後服務申請對外Id equals p.f售後服務申請對外Id
//                         join q in db.t產品
//                         on p.f產品ID equals q.f產品ID
//                         join r in db.t店家
//                         on q.f店家ID equals r.f店家ID
//                         select new CexchangeRecord
//                         {
//                             f申請日期 = o.f申請日期,
//                             f售後服務申請對外Id = o.f售後服務申請對外Id,
//                             f店家名稱 = q.f店家名稱,
//                             f換貨申請狀態 = o.f換貨申請狀態,
//                             f對外產品識別ID = q.f對外產品識別ID,
//                             f產品名稱 = q.f產品名稱,
//                             f換貨數量 = p.f換貨數量,
//                             f店家連絡電話 = r.f店家連絡電話,
//                             f地址 = r.f地址,
//                             f換貨申請人 = o.f換貨申請人
//                         }).ToList();
//            return View(query);
//        }  //OK
//        [HttpPost]
//        public ActionResult Mexchange(string txtKeyword, string choose)
//        {

//            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
//            {
//                ViewBag.data = "請輸入關鍵字";
//                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
//                return View(empty);
//            }

//            switch (choose)
//            {
//                case "customer":
//                    var query = (from o in db.t店家
//                                 join q in db.t換貨
//                                 on o.f店家ID equals q.f店家ID
//                                 join p in db.t換貨
//                                 on q.f店家ID equals p.f店家ID
//                                 orderby o.f店家ID
//                                 where o.f店家名稱.Contains(txtKeyword)
//                                 select new CexchangeRecord
//                                 {
//                                     f申請日期 = q.f申請日期,
//                                     f售後服務申請對外Id = q.f售後服務申請對外Id,
//                                     f店家名稱 = o.f店家名稱,
//                                     f換貨申請狀態 = q.f換貨申請狀態

//                                 }).ToList();

//                    return View(query);

//                case "order":
//                    var query2 = (from o in db.t店家
//                                  join q in db.t換貨
//                                  on o.f店家ID equals q.f店家ID
//                                  join p in db.t換貨
//                                  on q.f店家ID equals p.f店家ID
//                                  orderby o.f店家ID
//                                  where q.f售後服務申請對外Id.Contains(txtKeyword)
//                                  select new CexchangeRecord
//                                  {
//                                      f申請日期 = q.f申請日期,
//                                      f售後服務申請對外Id = q.f售後服務申請對外Id,
//                                      f店家名稱 = o.f店家名稱,
//                                      f換貨申請狀態 = q.f換貨申請狀態

//                                  }).ToList();

//                    return View(query2);

//                default:
//                    var query3 = (from o in db.t店家
//                                  join q in db.t換貨
//                                  on o.f店家ID equals q.f店家ID
//                                  join p in db.t換貨
//                                  on q.f店家ID equals p.f店家ID
//                                  orderby o.f店家ID
//                                  where q.f申請日期.Contains(txtKeyword)
//                                  select new CexchangeRecord
//                                  {
//                                      f申請日期 = q.f申請日期,
//                                      f售後服務申請對外Id = q.f售後服務申請對外Id,
//                                      f店家名稱 = o.f店家名稱,
//                                      f換貨申請狀態 = q.f換貨申請狀態,


//                                  }).ToList();

//                    return View(query3);

//            }

//        }  //OK

//        public ActionResult MexchangeDetail(string id)
//        {
//            if (string.IsNullOrEmpty(id))
//            {
//                return RedirectToAction("Mexchange");
//            }

//            var query = (from o in db.t換貨
//                         join p in db.t換貨明細
//                         on o.f換貨單號ID equals p.f換貨單號ID
//                         join q in db.t售後服務申請
//                         on o.f售後服務申請對外Id equals q.f售後服務申請對外Id

//                         where p.f售後服務申請對外Id == id
//                         select new CexchangeRecord
//                         {
//                             f申請日期 = o.f申請日期,
//                             f售後服務申請對外Id = o.f售後服務申請對外Id,
//                             f換貨申請狀態 = o.f換貨申請狀態,
//                             f產品名稱 = q.f產品名稱,
//                             f近視老花度數 = q.f近視老花度數,
//                             f閃光度數 = q.f閃光度數,
//                             f閃光角度 = q.f閃光角度,
//                             f數量 = q.f數量,
//                             f換貨原因 = o.f換貨原因,
//                             f換貨申請人 = o.f換貨申請人,
//                             //f店家連絡電話 = q.f店家連絡電話,
//                             //f地址 = q.f地址,
//                             f換貨備註 = o.f換貨備註,

//                         }).FirstOrDefault();


//            //f對外產品識別ID = r.f對外產品識別ID,
//            //f產品名稱 = r.f產品名稱,
//            //f換貨數量 = o.f數量,
//            //f店家連絡電話 = q.f店家連絡電話,
//            //f地址 = q.f地址,
//            //f換貨原因 = o.f換貨原因,
//            //f換貨備註 = o.f換貨備註



//            //foreach(var item in query)
//            //{

//            //    var checkOut = db.t產品.Where(
//            //        m => m.f產品名稱 == item.f產品名稱
//            //        && m.f近視老花度數 == item.f近視老花度數
//            //        && m.f閃光角度 == item.f閃光角度
//            //        && m.f閃光度數 == item.f閃光度數

//            //        );

//            //    var a = checkOut.Count();

//            //    if (a == 0)
//            //    {
//            //        item.f可換貨 = false;
//            //    }
//            //    else
//            //    {
//            //        int count = 0;
//            //        foreach (var checkOutResult in checkOut)
//            //        {
//            //            count += checkOutResult.f庫存數量;

//            //        }
//            //        if (count >= item.f換貨數量)
//            //        {
//            //            item.f可換貨 = true;
//            //        }
//            //    }
//            //}


//            return View(query);

//        }//判斷庫存

//        [HttpGet]
//        public ActionResult Cexchange()
//        {
//            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
//            var query = (from o in db.t換貨
//                         join p in db.t店家
//                         on o.f店家ID equals p.f店家ID
//                         where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin
//                         select new CexchangeRecord
//                         {
//                             f申請日期 = o.f申請日期,
//                             f售後服務申請對外Id = o.f售後服務申請對外Id,
//                             f換貨申請狀態 = o.f換貨申請狀態

//                         }).ToList();
//            return View(query);
//        }//OK
//        [HttpPost]
//        public ActionResult Cexchange(string txtKeyword, string choose)
//        {
//            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);

//            if (string.IsNullOrEmpty(Request.Form["txtKeyword"]))
//            {
//                ViewBag.data = "請輸入關鍵字";
//                List<contact.ViewModels.CexchangeRecord> empty = new List<CexchangeRecord>();
//                return View(empty);
//            }

//            switch (choose)
//            {
//                case "date":
//                    var query = (from o in db.t換貨
//                                 join p in db.t店家
//                                 on o.f店家ID equals p.f店家ID
//                                 where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin && o.f申請日期.Contains(txtKeyword)
//                                 select new CexchangeRecord
//                                 {
//                                     f申請日期 = o.f申請日期,
//                                     f售後服務申請對外Id = o.f售後服務申請對外Id,
//                                     f換貨申請狀態 = o.f換貨申請狀態
//                                 }).ToList();

//                    return View(query);

//                default:
//                    var query2 = (from o in db.t換貨
//                                  join p in db.t店家
//                                  on o.f店家ID equals p.f店家ID
//                                  where o.f店家ID == accoutLogin && p.f店家ID == accoutLogin && o.f售後服務申請對外Id.Contains(txtKeyword)

//                                  select new CexchangeRecord
//                                  {
//                                      f申請日期 = o.f申請日期,
//                                      f售後服務申請對外Id = o.f售後服務申請對外Id,
//                                      f換貨申請狀態 = o.f換貨申請狀態
//                                  }).ToList();

//                    return View(query2);


//            }
//        }//OK
//        public ActionResult CexchangeDetail(string id)
//        {
//            if (string.IsNullOrEmpty(id))
//            {
//                return RedirectToAction("Cexchange");
//            }
//            var query = (from o in db.t換貨
//                         join p in db.t換貨明細
//                         on o.f換貨單號ID equals p.f換貨單號ID
//                         join q in db.t售後服務申請
//                         on o.f售後服務申請對外Id equals q.f售後服務申請對外Id
//                         where p.f售後服務申請對外Id == id
//                         select new CexchangeRecord
//                         {
//                             f申請日期 = o.f申請日期,
//                             f售後服務申請對外Id = o.f售後服務申請對外Id,
//                             f換貨申請狀態 = o.f換貨申請狀態,
//                             f產品名稱 = q.f產品名稱,
//                             f近視老花度數 = q.f近視老花度數,
//                             f閃光度數 = q.f閃光度數,
//                             f閃光角度 = q.f閃光角度,
//                             f數量 = q.f數量,
//                             f換貨原因 = o.f換貨原因,
//                             //f換貨備註 = o.f換貨備註
//                         }).FirstOrDefault();

//            return View(query);
//        }//OK

//        public ActionResult flashA(string colors, string pName, string pShort, string pFlash)
//        {
//            var pColors = db.t產品.Where(m => m.f產品名稱 == pName && m.f產品顏色 == colors && m.f近視老花度數 == pShort && m.f閃光度數 == pFlash).Select(n => new
//            {
//                n.f閃光角度
//            }).Distinct().OrderBy(n => n.f閃光角度);
//            return Json(pColors, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult flashP(string colors, string pName, string pShort)
//        {
//            var pColors = db.t產品.Where(m => m.f產品名稱 == pName && m.f產品顏色 == colors && m.f近視老花度數 == pShort).Select(n => new
//            {
//                n.f閃光度數
//            }).Distinct().OrderBy(n => n.f閃光度數);
//            return Json(pColors, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult shortP(string colors, string pName)
//        {
//            var pColors = db.t產品.Where(m => m.f產品名稱 == pName && m.f產品顏色 == colors).Select(n => new
//            {
//                n.f近視老花度數
//            }).Distinct().OrderBy(n => n.f近視老花度數);
//            return Json(pColors, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult Colors(string pName)
//        {
//            var pColors = db.t產品.Where(m => m.f產品名稱 == pName).Select(n => new
//            {
//                n.f產品顏色
//            }).Distinct().OrderBy(n => n.f產品顏色);
//            return Json(pColors, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult Product(string id)
//        {
//            var pprodduct = db.t產品.Select(n => new
//            {

//                n.f產品名稱
//            }).Distinct().OrderBy(n => n.f產品名稱);

//            return Json(pprodduct, JsonRequestBehavior.AllowGet);
//        }
//        [HttpGet]
//        public ActionResult Cexchangecreate(string id, string pid) //OK 
//        {

//            if (string.IsNullOrEmpty(id))
//            {
//                return RedirectToAction("COderDetail");
//            }
//            else
//            {
//                var query = from o in db.t訂單明細
//                            join b in db.t訂單
//                            on o.f訂單單號ID equals b.f訂單單號ID
//                            join q in db.t產品
//                            on o.f產品ID equals q.f產品ID
//                            where b.f對外訂單單號 == id
//                            select new MOderDetailRecord
//                            {
//                                f產品名稱 = q.f產品名稱,
//                                f訂購數量 = o.f訂購數量,
//                                f售價 = q.f售價,
//                                f產品顏色 = q.f產品顏色,
//                                f近視老花度數 = q.f近視老花度數,
//                                f閃光度數 = q.f閃光度數,
//                                f閃光角度 = q.f閃光角度,
//                                f對外產品識別ID = q.f對外產品識別ID,
//                                f品牌名稱 = q.f品牌名稱
//                            };
//                ExchangeProduct product = new ExchangeProduct();
//                ExchangeProduct notFlash = new ExchangeProduct();

//                product.AfterSalesService = query;
//                notFlash.AfterSalesService = query;

//                var productId = (from o in db.t產品
//                                 where o.f對外產品識別ID == pid
//                                 select o.f產品ID).FirstOrDefault();

//                var detailInt = (from o in db.t訂單
//                                 where o.f對外訂單單號 == id
//                                 select o.f訂單單號ID).FirstOrDefault();

//                var brand = db.t產品.Find(productId).f品牌名稱;
//                var colors = db.t產品.Find(productId).f產品顏色;
//                var pName = db.t產品.Find(productId).f產品名稱;
//                ViewBag.pName = pName;
//                var pShortsighteds = db.t產品.Find(productId).f近視老花度數;
//                var pflashs = db.t產品.Find(productId).f閃光度數;
//                product.brand = brand;
//                notFlash.brand = brand;


//                var color = db.t產品.Where(m => m.f產品名稱 == pName).GroupBy(c => c.f產品顏色).Select(s => s.Key);
//                var Shortsighted = db.t產品.Where(m => m.f產品顏色 == colors && m.f產品名稱 == pName).GroupBy(c => c.f近視老花度數).Select(s => s.Key);
//                var flash = db.t產品.Where(m => m.f產品顏色 == colors && m.f產品名稱 == pName && m.f近視老花度數 == pShortsighteds).GroupBy(c => c.f閃光度數).Select(s => s.Key);
//                var flashAstigmatism = db.t產品.Where(m => m.f產品顏色 == colors && m.f產品名稱 == pName && m.f近視老花度數 == pShortsighteds && m.f閃光度數 == pflashs).GroupBy(c => c.f閃光角度).Select(s => s.Key);



//                product.allColors = color;
//                product.allflash = flash;
//                product.allflashAstigmatism = flashAstigmatism;
//                product.allShortsighted = Shortsighted;


//                var toFlash = db.t產品.Where(m => m.f對外產品識別ID == pid).FirstOrDefault().f閃光度數;


//                double Toflash;
//                bool test = double.TryParse(toFlash, out Toflash);



//                if (Toflash > 0)
//                {
//                    product.isPositive = true;
//                    return View(product);
//                }
//                notFlash.allColors = color;
//                notFlash.allShortsighted = Shortsighted;
//                notFlash.allflash = flash;
//                notFlash.allflashAstigmatism = flashAstigmatism;
//                notFlash.isPositive = false;

//                return View(notFlash);

//            }
//            return RedirectToAction("COderDetail");
//        }

//        [HttpPost]
//        public ActionResult Cexchangecreate(string exchangeOutId, string id, string pid,
//            string f產品顏色, string f近視老花度數, string f閃光角度, string f閃光度數,
//            int nums, MOderDetailRecord cproduct, string f換貨原因, string f換貨備註, decimal? price, string f產品名稱, string f申請日期, int f店家ID, string f售後服務申請對外Id)
//        {

//            var accoutLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
//            var detailInt = (from o in db.t訂單
//                             where o.f對外訂單單號 == id
//                             select o.f訂單單號ID).FirstOrDefault();


//            var dFid = (from o in db.t訂單明細
//                        where o.f訂單單號ID == detailInt
//                        select o.f訂單明細ID).FirstOrDefault();

//            var customerId = (from o in db.t訂單
//                              where o.f訂單單號ID == detailInt
//                              select o.f店家ID).FirstOrDefault();

//            var customerName = (from o in db.t店家
//                                where o.f店家ID == customerId
//                                select o.f店家名稱).FirstOrDefault();


//            var customerTel = (from o in db.t店家
//                               where o.f店家ID == customerId
//                               select o.f店家連絡電話).FirstOrDefault();


//            var productId = (from o in db.t產品
//                             where o.f對外產品識別ID == pid
//                             select o.f產品ID).FirstOrDefault();


//            var exChangeId = (from o in db.t產品
//                              where o.f產品名稱 == f產品名稱 && o.f產品顏色 == f產品顏色 && o.f近視老花度數 == f近視老花度數 && o.f閃光度數 == f閃光度數 && o.f閃光角度 == f閃光角度
//                              select o.f產品ID).FirstOrDefault();

//            var cName = (from o in db.t產品
//                         where o.f產品ID == productId
//                         select o.f店家名稱).FirstOrDefault();


//            var cTel = (from o in db.t店家
//                        where o.f店家名稱 == cName
//                        select o.f店家連絡電話).FirstOrDefault();


//            //var cId = (from o in db.t店家
//            //           where o.f店家名稱 == cName
//            //           select o.f店家ID).FirstOrDefault();


//            var changedId = (from o in db.t換貨
//                             where o.f售後服務申請對外Id == f售後服務申請對外Id
//                             select o.f換貨單號ID).FirstOrDefault();

//            var productIdPrice = (from o in db.t產品
//                                  where o.f對外產品識別ID == pid
//                                  select o.f售價).FirstOrDefault();
//            var pName = (from o in db.t產品
//                         where o.f產品ID == productId
//                         select o.f產品名稱).FirstOrDefault();
//            var pColor = (from o in db.t產品
//                          where o.f產品ID == productId
//                          select o.f產品顏色).FirstOrDefault();
//            var pShort = (from o in db.t產品
//                          where o.f產品ID == productId
//                          select o.f近視老花度數).FirstOrDefault();
//            var pFlash = (from o in db.t產品
//                          where o.f產品ID == productId
//                          select o.f閃光度數).FirstOrDefault();
//            var pAstigmatism = (from o in db.t產品
//                                where o.f產品ID == productId
//                                select o.f閃光角度).FirstOrDefault();
//            var chId = (from o in db.t換貨明細
//                        where o.f產品ID == productId
//                        select o.f換貨單號ID).FirstOrDefault();
//            var cNameId = (from o in db.t換貨
//                           where o.f換貨單號ID == chId
//                           select o.f店家ID).FirstOrDefault();

//            t售後服務申請 after = new t售後服務申請();
//            after.f售後服務申請對外Id = exchangeOutId;
//            after.f數量 = nums;
//            after.f產品名稱 = f產品名稱;
//            after.f產品顏色 = cproduct.f產品顏色;
//            after.f近視老花度數 = cproduct.f近視老花度數;
//            after.f閃光度數 = cproduct.f閃光度數;
//            after.f閃光角度 = cproduct.f閃光角度;
//            after.f換貨單價 = price;
//            after.f換貨原因 = f換貨原因;
//            after.f換貨備註 = f換貨備註;

//            db.t售後服務申請.Add(after);

//            t換貨明細 newChangeDetail = new t換貨明細();
//            newChangeDetail.f產品ID = exChangeId;
//            newChangeDetail.f要換的產品ID = exChangeId;
//            newChangeDetail.f換貨數量 = nums;
//            newChangeDetail.f要換的數量 = nums;
//            newChangeDetail.f換貨單價 = price;
//            newChangeDetail.f要換的產品單價 = price;
//            newChangeDetail.f售後服務申請對外Id = exchangeOutId;
//            newChangeDetail.f換貨單號ID = changedId;

//            db.t換貨明細.Add(newChangeDetail);


//            f申請日期 = DateTime.Now.ToString("yyyy-MM-dd");
//            t換貨 exchanging = new t換貨();
//            exchanging.f申請日期 = f申請日期;
//            exchanging.f換貨原因 = f換貨原因;
//            exchanging.f換貨備註 = f換貨備註;
//            exchanging.f換貨申請人 = customerName;
//            exchanging.f連絡電話 = customerTel;
//            //exchanging.f店家ID = cId;
//            exchanging.f店家ID = accoutLogin;
//            exchanging.f對外訂單單號 = id;
//            exchanging.f訂單明細ID = dFid;
//            exchanging.f換貨申請狀態 = "未出貨";
//            exchanging.f售後服務申請對外Id = exchangeOutId;


//            db.t換貨.Add(exchanging);

//            db.SaveChanges();

//            return RedirectToAction("COderDetail");
//        }

//        public ActionResult CexchangeDelete(string id)
//        {
//            var cancelId = from o in db.t換貨
//                           where id == o.f售後服務申請對外Id
//                           select o;
//            foreach (var item in cancelId)
//            {
//                if (item.f換貨申請狀態 == "未出貨" && item.f換貨申請狀態 != "取消")
//                {
//                    item.f換貨申請狀態 = "取消";
//                }

//            }
//            db.SaveChanges();
//            return RedirectToAction("Cexchange");

//        } //OK


//    }
//}