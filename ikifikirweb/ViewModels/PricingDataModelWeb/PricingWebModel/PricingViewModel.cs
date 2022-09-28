using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.PricingDataModelWeb.PricingWebModel
{
    public class PricingViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public decimal MonthPrice { get; set; }
        public decimal YearPrice { get; set; }
    }
}
