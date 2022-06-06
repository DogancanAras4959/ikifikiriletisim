using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("tagProject")]
    public class tagProject : IEntity
    {
        public tagProject()
        {

        }

        public int Id { get; set; }

        [ForeignKey("projectToTag")]
        public int tagId { get; set; }

        [ForeignKey("tagToTag")]
        public int projectId { get; set; }

        public project projectToTag { get; set; }
        public tags tagToTag { get; set; }
    }
}
