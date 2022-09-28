using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("pricingComponents")]
    public class pricingComponents : GeneralModel, IEntity
    {
        public pricingComponents()
        {
            pricingComponentTypeList = new List<pricingComponentTypes>();
        }

        public string ComponentTitle { get; set; }

        [ForeignKey("pricing")]
        public int PricingId { get; set; }
        public bool ChooseType { get; set; }

        public decimal Price { get; set; }

        public pricing pricing { get; set; }

        public ICollection<pricingComponentTypes> pricingComponentTypeList { get; set; }
    }
}
