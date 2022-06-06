using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{

    [Table("teams")]
    public class teams : GeneralModel, IEntity
    {
        public teams()
        {

        }

        public string name { get; set; }
        public string image { get; set; }
        public string role { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string gmail { get; set; }
    }
}
