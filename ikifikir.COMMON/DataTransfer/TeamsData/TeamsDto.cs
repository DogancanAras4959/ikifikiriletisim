using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.COMMON.DataTransfer.TeamsData
{
    public class TeamsDto : BaseDto
    {
        public string name { get; set; }
        public string image { get; set; }
        public string role { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string gmail { get; set; }
    }
}
