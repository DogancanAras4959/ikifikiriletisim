using ikifikir.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL.Models
{
    public class referenceLogo : GeneralModel, IEntity
    {
        public referenceLogo()
        {

        }

        public string slug { get; set; }
        public int sorted { get; set; }

    }
}
