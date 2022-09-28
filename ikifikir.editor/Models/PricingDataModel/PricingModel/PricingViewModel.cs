using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.PricingDataModel.PricingModel
{
    public class PricingViewModel : BaseViewModel
    {
        public PricingViewModel()
        {
            pricingComponentList = new List<pricingComponents>();
        }

        public string Title { get; set; }
        public bool Status { get; set; }
        public decimal MonthPrice { get; set; }
        public string Image { get; set; }
        public decimal YearPrice { get; set; }
     
        List<pricingComponents> pricingComponentList { get; set; }
    }
}
