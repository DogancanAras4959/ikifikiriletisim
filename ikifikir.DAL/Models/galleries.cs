using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("galleries")]
    public class galleries : GeneralModel, IEntity
    {
        public galleries()
        {

        }

        public string slug { get; set; }
        public int sorted { get; set; }

        [ForeignKey("projectToGalleries")]
        public int projectId { get; set; }
        public project projectToGalleries { get; set; }

    }
}
