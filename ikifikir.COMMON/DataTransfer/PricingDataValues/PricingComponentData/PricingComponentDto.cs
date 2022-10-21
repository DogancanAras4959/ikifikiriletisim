using ikifikir.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.PricingDataValues.PricingComponentData
{
    public class PricingComponentDto : BaseDto
    {
        public string ComponentTitle { get; set; }
        public int PricingId { get; set; }
        public bool ChooseType { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public pricing pricing { get; set; }
        public List<pricingComponentTypes> pricingComponentTypesList { get; set; }
    }
}
