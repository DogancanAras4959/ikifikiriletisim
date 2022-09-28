using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("category")]
    public class category : GeneralModel, IEntity
    {
        public category()
        {
            projects = new List<project>();
        }

        public string name { get; set; }
        public string filterType { get; set; }
        public string categoryTags { get; set; }
        public virtual ICollection<project> projects { get; set; }

    }
}
