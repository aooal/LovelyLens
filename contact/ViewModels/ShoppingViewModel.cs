using contact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class ShoppingViewModel
    {
        public t產品 product { get; set; }
        public List<string> colorList { get; set; }
    }
}