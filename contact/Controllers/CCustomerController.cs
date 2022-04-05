using contact.Models;
using contact.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace contact.Controllers
{
    public class CCustomerController : Controller
    {
        DBEyeEntities2 db = new DBEyeEntities2();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            List<CShoppingCartItem> cart = Session[SessionKeys.SK_SHOPPINGCART_ITEMLIST] as List<CShoppingCartItem>;
            return View(cart);
        }
        [HttpPost]
        public ActionResult CheckOut(t訂單 viewModel)
        {
            //先寫入t訂單
            viewModel.f店家ID = Convert.ToInt32(Request.Cookies["USERid"].Value);
            var random = new Random();
            string outerOrderNum = "C" + viewModel.f店家ID.ToString() + DateTime.Now.ToString("yyyymmddhhmm");
            viewModel.f對外訂單單號 = outerOrderNum;

            db.t訂單.Add(viewModel);
            db.SaveChanges();
            //再寫入t訂單明細
            //id=對外訂單單號
            return RedirectToAction("OrderDetail", new { id = outerOrderNum });
        }

        public ActionResult OrderDetail(string id)
        {
            var selOrder = db.t訂單.FirstOrDefault(o => o.f對外訂單單號 == id);
            var orderID = selOrder.f訂單單號ID;
            List<CShoppingCartItem> cart = Session[SessionKeys.SK_SHOPPINGCART_ITEMLIST] as List<CShoppingCartItem>;
            var orderDetailItem = new t訂單明細();
            foreach (var item in cart)
            {
                orderDetailItem.f訂單單號ID = orderID;
                orderDetailItem.f產品ID = item.Product.f產品ID;
                orderDetailItem.f訂購數量 = item.數量;
                orderDetailItem.f單價 = item.單價;
                db.t訂單明細.Add(orderDetailItem);
                db.SaveChanges();
            }
            return RedirectToAction("../Shopping/List");
        }

    public ActionResult getCookie()
    {
        var idLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
        DBEyeEntities2 db = new DBEyeEntities2();
        var accountData = db.t店家.FirstOrDefault(m => m.f店家ID == idLogin);
        var datas = new CAutoFillin();
        datas.f訂購人 = accountData.f店家名稱;
        datas.f連絡電話 = accountData.f店家連絡電話;
        datas.f寄送地址 = accountData.f地址;
        datas.f訂購人信箱 = accountData.f電子信箱;
        return Json(datas, JsonRequestBehavior.AllowGet);
    }
    }




}