using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentTypeData
{
    public class PricingComponentTypeDto : BaseDto
    {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int pricingComponentId { get; set; }
        public pricingComponents pricingComponents { get; set; }
    }
}
