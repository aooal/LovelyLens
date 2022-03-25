using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.Models
{
    public class CShoppingCartItem
    {
        public string 品牌名稱 { get; set; }
        public string 產品名稱 { get; set; }
        public string 產品顏色 { get; set; }
        public string 近視度數 { get; set; }
        public string 散光度數 { get; set; }
        public string 散光角度 { get; set; }
        public int 數量 { get; set; }
        public decimal 單價 { get; set; }
        public decimal 小計 { get { return this.數量 * this.單價; } }
        public t產品 Product { get; set; }
    }
}