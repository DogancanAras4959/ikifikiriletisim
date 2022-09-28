using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.PricingDataModel.PricingComponentModel
{
    public class PricingComponentViewModel : BaseViewModel
    {
        public PricingComponentViewModel()
        {
            pricingComponentTypesList = new List<pricingComponentTypes>();
        }

        public string ComponentTitle { get; set; }
        public int PricingId { get; set; }
        public decimal Price { get; set; }
        public bool ChooseType { get; set; }
        public pricing pricing { get; set; }
        public List<pricingComponentTypes> pricingComponentTypesList { get; set; }
    }
}
