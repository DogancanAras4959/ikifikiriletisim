using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikifikirweb.ViewModels.EmailModel
{
    public class EmailViewModel
    {
        public string to { get; set; }
        public string namesurname { get; set; }
        public string from { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
    }
}
