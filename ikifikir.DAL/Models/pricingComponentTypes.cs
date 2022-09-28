using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("pricingComponentTypes")]
    public class pricingComponentTypes : GeneralModel, IEntity
    {
        public pricingComponentTypes()
        {

        }

        public string Type { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("pricingComponents")]
        public int pricingComponentId { get; set; }
        public pricingComponents pricingComponents { get; set; }
    }
}
