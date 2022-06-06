using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("videos")]
    public class videos : GeneralModel, IEntity
    {
        public videos()
        {

        }

        public string slug { get; set; }
        public string name { get; set; }
        public string iframe { get; set; }

        [ForeignKey("projectVideos")]
        public int projectId { get; set; }
        public project projectVideos { get; set; }
    }
}
