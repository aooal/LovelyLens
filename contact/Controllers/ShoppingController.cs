using contact.Models;
using contact.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;


namespace contact.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult index()
        {
            return View();
        }
        public ActionResult List(string keyword, int? page)
        {
            DBEyeEntities2 db = new DBEyeEntities2();

            //var Products = (from p in (new DBEyeEntities()).t產品 select p.f產品名稱).Distinct(new ProductNameComparer());
            //var Products = new DBEyeEntities().t產品.Distinct(new ProductNameComparer());
            //var nameList = (from prod in new DBEyeEntities().t產品 select prod.f產品名稱).Distinct().ToList();
            //var Products = new DBEyeEntities().t產品.Where(m => nameList.Contains(m.f產品名稱));

            var Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault());

            //var Products = db.t產品.Join(db.t優惠活動,
            //    a => a.f產品ID,
            //    b => b.f優惠活動ID,
            //    (a, b) => new
            //    {
            //        產品名稱 = a.f產品名稱,
            //        品牌名稱 = a.f品牌名稱,
            //        活動照片路徑 = b.f活動照片路徑,
            //        ID = a.f產品ID
            //    }).OrderBy(ab => ab.ID).GroupBy(ab => ab.產品名稱).Select(p => p.FirstOrDefault());

            string keyword = Request.Form["txtKeyword"];

            string theBrand = Request.Form["txtBrand"];
            int a = 10;
            Console.WriteLine(theBrand);

            if (string.IsNullOrEmpty(keyword))
            {
                Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault());
            }
            else
            {
                Products = db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault()).
                   Where(p => p.f產品名稱.Contains(keyword) || p.f品牌名稱.Contains(keyword));
                a = Products.Count();

            }


            return View(Products.ToList().ToPagedList(page ?? 1, a));
        }

        [ChildActionOnly]
        public ActionResult _activDisplay(int count = 0)
        {
            DBEyeEntities2 db = new DBEyeEntities2();
            List<t優惠活動> activPhoto;
            activPhoto = (from p in db.t優惠活動 orderby p.f活動照片路徑 select p).Take(count).ToList();
            return PartialView("_activDisplay", activPhoto);
        }

        [ChildActionOnly]
        public ActionResult _pPhoto(int? id)
        {
            DBEyeEntities2 db = new DBEyeEntities2();
            t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == id);
            //List<t產品> thePhoto;
            if (prod != null)
            {
                //return View(prod);
                string fname = prod.f產品名稱;
                string fcolor = prod.f產品顏色;

                var result = (db.t產品.Where(m => m.f產品名稱 == fname)).GroupBy(m => m.f產品圖片路徑).Select(p => p.FirstOrDefault());
                return PartialView("_pPhoto", result);
            }
            return RedirectToAction("Detail");
            //thePhoto = (from p in db.t產品 orderby p.f產品圖片路徑 select p).Take(3).ToList();
            //thePhoto = (db.t產品.GroupBy(m => m.f產品名稱).Select(p => p.FirstOrDefault()).OrderBy(p => p.f產品圖片路徑)).Take(3).ToList();
            //return PartialView("_pPhoto", thePhoto);
        }

        public ActionResult Detail(int? id)
        {
            if (id != null)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                t產品 prod = db.t產品.FirstOrDefault(p => p.f產品ID == id);
                if (prod != null)
                {
                    //return View(prod);
                    //string fname = prod.f產品名稱;

                    var result = db.t產品.Where(m => m.f產品名稱 == prod.f產品名稱);
                    return View(result);
                }
            }
            return RedirectToAction("Detail");
        }

        [HttpPost]
        public ActionResult Detail(CAddToCartViewModel viewModel)
        {
            DBEyeEntities2 db = new DBEyeEntities2();
            if (viewModel.f閃光度數 == "散光度數")
            {
                viewModel.f閃光度數 = "0";
            }
            if (viewModel.f閃光角度 == "散光角度")
            {
                viewModel.f閃光角度 = "0";
            }

            var product = from p in db.t產品
                          where (
                           p.f品牌名稱 == viewModel.f品牌名稱 &
                           p.f產品名稱 == viewModel.f產品名稱 &
                           p.f產品顏色 == viewModel.f產品顏色 &
                           p.f近視老花度數 == viewModel.f近視老花度數 &
                           p.f閃光度數 == viewModel.f閃光度數 &
                           p.f閃光角度 == viewModel.f閃光角度)
                          select p;
            var prod = product.FirstOrDefault();
            if (prod != null)
            {
                List<CShoppingCartItem> cartItems = Session[SessionKeys.SK_SHOPPINGCART_ITEMLIST] as List<CShoppingCartItem>;
                if (cartItems == null)
                {
                    cartItems = new List<CShoppingCartItem>();
                    Session[SessionKeys.SK_SHOPPINGCART_ITEMLIST] = cartItems;
                }
                string dis = "";
                if (viewModel.數量 < prod.f庫存數量 * 0.8)
                {
                    dis = "足夠";

                }
                else
                {
                    dis = ((int)(prod.f庫存數量 * 0.8)).ToString();
                    if (Convert.ToInt32(dis) < 1)
                    {
                        dis = "0";
                    }
                }
                cartItems.Add(new CShoppingCartItem()
                {
                    品牌名稱 = prod.f品牌名稱,
                    產品名稱 = prod.f產品名稱,
                    產品顏色 = prod.f產品顏色,
                    近視度數 = prod.f近視老花度數,
                    散光度數 = prod.f閃光度數,
                    散光角度 = prod.f閃光角度,
                    單價 = Convert.ToDecimal(prod.f售價),
                    數量 = viewModel.數量,
                    可訂購數量 = dis,
                    Product = prod
                });
            }

            return RedirectToAction("List");
        }

        public ActionResult _theActivity(int? id)
        {
            if (id != null)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                t優惠活動 prod = db.t優惠活動.FirstOrDefault(p => p.f優惠活動ID == id);
                if (prod != null)
                {

                    return PartialView("_theActivity", prod);
                }
            }
            return RedirectToAction("List");
        }

        public ActionResult CartView()
        {
            var accountID = Convert.ToInt32(Request.Cookies["USERid"].Value);
            DBEyeEntities2 db = new DBEyeEntities2();
            var accountLogin = db.t店家.FirstOrDefault(m => m.f店家ID == accountID);
            List<CShoppingCartItem> cart = Session[SessionKeys.SK_SHOPPINGCART_ITEMLIST] as List<CShoppingCartItem>;
            if (cart == null)
                return RedirectToAction("List");
            ViewBag.AccountName = accountLogin.f店家名稱;
            ViewBag.AccountMobile = accountLogin.f店家連絡電話;
            ViewBag.AccountAddress = accountLogin.f地址;
            ViewBag.AccountEmail = accountLogin.f電子信箱;
            return View(cart);
        }

    }
}