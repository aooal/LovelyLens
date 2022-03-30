﻿using contact.Models;
using contact.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace contact.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CLoginInfo vModel)
        {
            DBEyeEntities2 db = new DBEyeEntities2();
            t店家 selectAccount = db.t店家.FirstOrDefault(m => m.f電子信箱 == vModel.loginAccount);

            if (selectAccount != null && selectAccount.f密碼 == vModel.loginPassword)
            {
                HttpCookie cookie = Request.Cookies["USERid"];
                Session["USERid"] = selectAccount.f店家ID;
                cookie = new HttpCookie("USERid");
                cookie.Value = selectAccount.f店家ID.ToString();
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

                ViewBag.Account = selectAccount.f店家ID;
                ViewBag.AccountName = selectAccount.f店家名稱;

                if (selectAccount.f身分別 == "管理者")
                {
                    return RedirectToAction("../CManager/Index"); //重新修改至管理者首頁
                }
                else if (selectAccount.f往來狀態 == "黑名單")
                {
                    return Content("帳號目前無法使用，請洽詢業務負責人：\n周沛妤\n0988-888-888");
                }
                else
                {
                    return RedirectToAction("../Shopping/list");
                }
            }
            else
            {
                return View(); //修改："登入錯誤訊息"
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            var accountLogin = Convert.ToInt32(Request.Cookies["USERid"].Value);
            DBEyeEntities2 db = new DBEyeEntities2();
            var accountName = db.t店家.FirstOrDefault(m => m.f店家ID == accountLogin).f店家名稱;
            return Content(accountName);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(t店家 註冊)
        {
            HttpPostedFileBase medicalLicense = Request.Files["medicalLicense"];
            HttpPostedFileBase businessLicense = Request.Files["businessLicense"];

            if (ModelState.IsValid)
            {
                DBEyeEntities2 db = new DBEyeEntities2();
                ViewBag.Occupied = false;
                var occupied_check = db.t店家.Where(m => m.f電子信箱 == 註冊.f電子信箱).FirstOrDefault();
                if (occupied_check != null)
                {
                    ViewBag.Occupied = true;
                    return Content("帳號已被使用"); /****** 暫時用 RETURN CONTENT ******/
                }

                //圖片上傳
                //藥許
                if (medicalLicense != null)
                {
                    var MLFileName = Path.GetRandomFileName().Replace(".", "_") + ".jpg";
                    var MLPath = Path.Combine(Server.MapPath("~/images/iMedicalLicense"), MLFileName);
                    medicalLicense.SaveAs(MLPath);
                    註冊.f藥商許可證照片路徑 = MLFileName;
                }
                //營許
                if (businessLicense != null)
                {
                    var BLFileName = Path.GetRandomFileName().Replace(".", "_") + ".jpg";
                    var BLPath = Path.Combine(Server.MapPath("~/images/iBusinessLicense"), BLFileName);
                    businessLicense.SaveAs(BLPath);
                    註冊.f營業登記許可照片路徑 = BLFileName;
                }

                註冊.f身分別 = "經銷商";
                註冊.f往來狀態 = "交易中";
                註冊.f註冊日期 = DateTime.Now.ToString("yyyyMMddHHmmss");
                db.t店家.Add(註冊);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(註冊);
        }
        public ActionResult AccUpdates()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Management()
        {
            return View();
        }
    }
}