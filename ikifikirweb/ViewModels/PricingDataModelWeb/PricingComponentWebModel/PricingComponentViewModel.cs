using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.PricingDataModelWeb.PricingComponentWebModel
{
    public class PricingComponentViewModel : BaseViewModel
    {
        public string ComponentTitle { get; set; }
        public int PricingId { get; set; }
        public bool ChooseType { get; set; }
        public decimal Price { get; set; }
        public pricing pricing { get; set; }
    }
}
