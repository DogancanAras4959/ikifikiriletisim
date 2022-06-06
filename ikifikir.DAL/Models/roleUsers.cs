using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("roleUsers")]
    public class roleUsers : IEntity
    {
        public roleUsers()
        {

        }
        public int Id { get; set; }

        [ForeignKey("role")]
        public int roleId { get; set; }


        [ForeignKey("user")]
        public int userId { get; set; }

        public user user { get; set; }
        public roles role { get; set; }
    }
}
