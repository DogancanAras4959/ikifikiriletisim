using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    [Table("post")]
    public class post : GeneralModel, IEntity
    {
        public post()
        {
           
        }

        public string title { get; set; }
        public string spot { get; set; }
        public string content { get; set; }
        public int sorted { get; set; }
        public bool isNotification { get; set; }
        public string seoTitle { get; set; }
        public string seoSpot { get; set; }
        public string keywords { get; set; }
        public string image { get; set; }
        public string author { get; set; }
    
    }
}
