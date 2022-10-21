using ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.PricingDataValues.PricingData
{
    public class PricingDto : BaseDto
    {
        public string Title { get; set; }
        public bool Status { get; set; }
        public decimal MonthPrice { get; set; }
        public string Image { get; set; }
        public decimal YearPrice { get; set; }

    }
}
