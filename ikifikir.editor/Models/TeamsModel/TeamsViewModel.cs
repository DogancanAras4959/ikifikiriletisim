using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikir.editor.Models.TeamsModel
{
    public class TeamsViewModel : BaseViewModel
    {
        public string name { get; set; }
        public string role { get; set; }
        public string image { get; set; }
        public string facebook { get; set; }
        public string twitter { get; set; }
        public string instagram { get; set; }
        public string gmail { get; set; }
    }
}
