using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("tags")]
    public class tags : IEntity
    {
        public tags()
        {
            tagProjects = new List<tagProject>();
        }
        public int Id { get; set; }
        public string name { get; set; }

        public virtual ICollection<tagProject> tagProjects { get; set; }
    }
}
