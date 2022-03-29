using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class CexchangeRecord
    {
        public string f申請日期 { get; set; }
        public string f店家名稱 { get; set; }
        public Nullable<int> f換貨數量 { get; set; }
        public string f換貨原因 { get; set; }
        public string f換貨備註 { get; set; }
        public string f換貨申請狀態 { get; set; }
        public string f對外訂單單號 { get; set; }
        public Nullable<decimal> f訂單總金額 { get; set; }
        public string f地址 { get; set; }
        public string f店家連絡電話 { get; set; }
    }
}