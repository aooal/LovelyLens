using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class MOrderRecord
    {
       
        public string f訂購日期 { get; set; }
        public string f對外訂單單號 { get; set; }
        public int f訂單單號ID { get; set; }
        public string f店家名稱 { get; set; }
        public Nullable<decimal> f訂單總金額 { get; set; }

        public string f訂單狀態 { get; set; }

        public string f配送狀態 { get; set; }

        public string f付款狀態 { get; set; }
        public string f訂單需求 { get; set; }
        
        public string f產品名稱 { get; set; }
        public Nullable<int> f訂購數量 { get; set; }
        public Nullable<decimal> f售價 { get; set; }

        public string f對外產品識別ID { get; set; }

        public Nullable<decimal> f小計;
        public string f店家連絡電話 { get; set; }
        public string f地址 { get; set; }

        public string f訂單備註 { get; set; }
    }
}