using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class MOrderRecord
    {
        public int f產品ID { get; set; }
        public string f換貨申請狀態 { get; set; }
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

        //售後服務用
        public string f申請日期 { get; set; }
        public Nullable<int> f換貨數量 { get; set; }
        public int f售後服務申請Id { get; set; }
        public string f售後服務申請對外Id { get; set; }
        public string f產品顏色 { get; set; }
        public bool f可換貨 { get; set; }
        public string f近視老花度數 { get; set; }
        public string f閃光度數 { get; set; }
        public string f閃光角度 { get; set; }
        public Nullable<int> f庫存數量 { get; set; }
        public string f電子信箱 { get; set; }
        public string f換貨申請人 { get; set; }

    }
}