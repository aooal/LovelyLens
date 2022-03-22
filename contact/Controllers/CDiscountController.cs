using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
    }
}