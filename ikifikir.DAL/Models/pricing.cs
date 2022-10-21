using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("pricing")]
    public class pricing : GeneralModel, IEntity
    {
        public pricing()
        {
            pricingComponentsList = new List<pricingComponents>();
        }

        public string Title { get; set; }
        public string priceLongTitle { get; set; }

        public bool Status { get; set; }
        public decimal MonthPrice { get; set; }
        public string Image { get; set; }
        public decimal YearPrice { get; set; }

        public ICollection<pricingComponents> pricingComponentsList { get; set; }
    }
}
