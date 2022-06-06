using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("roles")]
    public class roles : GeneralModel, IEntity
    {
        public roles()
        {
            roleUserList = new List<roleUsers>();
        }

        public string roleName { get; set; }
        public virtual ICollection<roleUsers> roleUserList { get; set; }

    }
}
