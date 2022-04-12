using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class CexchangeDetailRecord
    {
        public int f換貨單號ID { get; set; }
        public Nullable<int> f訂購數量 { get; set; }
        public string f申請日期 { get; set; }
        public string f產品名稱 { get; set; }

        public string f訂購日期 { get; set; }
        public string f店家名稱 { get; set; }
        public string f訂單狀態 { get; set; }
        public string f換貨申請狀態 { get; set; }

        public string f對外訂單單號 { get; set; }
        public string f店家連絡電話 { get; set; }
        public string f地址 { get; set; }
        public string f品牌名稱 { get; set; }
        public string f對外產品識別ID { get; set; }

        public Nullable<int> f換貨數量 { get; set; }
        public Nullable<decimal> f訂單總金額 { get; set; }
        public int f換貨明細ID { get; set; }
        public Nullable<int> f產品ID { get; set; }      
        public Nullable<decimal> f換貨單價 { get; set; }
        public string f換貨原因 { get; set; }
        public string f訂單備註 { get; set; }
        public int f小計 { get; set; }
        public int subtotal = 0;
        //subtotal = f售價* f產品名稱
        public int f換貨總金額 { get; set; }
        public Nullable<decimal> f售價 { get; set; }
        public Nullable<decimal> f成本價 { get; set; }

        //規格
        public string f近視老花度數 { get; set; }
        public string f閃光度數 { get; set; }
        public string f閃光角度 { get; set; }
        public string f換貨備註 { get; set; }
        public List<string> f產品顏色 { get; set; }
    }
}