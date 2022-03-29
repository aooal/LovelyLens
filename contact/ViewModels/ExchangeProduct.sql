using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace contact.ViewModels
{
    public class ExchangeProduct
    {
        public bool isPositive { get; set; }
        public IQueryable<MOderDetailRecord> AfterSalesService { get; set; }
        public string brand { get; set; }
        public string fabc { get; set; }
        public IEnumerable<string> allColors { get; set; }
        public IEnumerable<string> allflash{ get; set; }

        public IEnumerable<string> allflashAstigmatism { get; set; }
        public IEnumerable<string> allShortsighted { get; set; }
    }
    
}

