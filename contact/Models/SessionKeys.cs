using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.Models
{
    public class SessionKeys
    {
        //登入資料 session key
        public static readonly string SESSION_KEY_USER = "SESSION_KEY_USER";

        //購物車 session keys
        public static readonly string SK_SHOPPINGCART_ITEMLIST = "SK_SHOPPINGCART_ITEMLIST";
    }
}