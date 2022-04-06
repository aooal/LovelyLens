using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class ShippingViewModels
    {

        public IEnumerable<MOrderRecord> query { get; set; }
        public IEnumerable<MOrderRecord> exchangeToShipping { get; set; }
    }


}

