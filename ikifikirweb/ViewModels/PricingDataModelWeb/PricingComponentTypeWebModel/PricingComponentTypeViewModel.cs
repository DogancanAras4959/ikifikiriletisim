using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentTypeWebModel
{
    public class PricingComponentTypeViewModel : BaseViewModel
    {
        public string Type { get; set; }
        public bool ChooseType { get; set; }
        public decimal Price { get; set; }
        public int pricingComponentId { get; set; }
        public pricingComponents pricingComponents { get; set; }
    }
}
