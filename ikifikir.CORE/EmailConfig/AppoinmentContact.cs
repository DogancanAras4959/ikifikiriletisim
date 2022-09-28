using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.CORE.EmailConfig
{
    public class AppoinmentContact
    {
        public AppoinmentContact()
        {
            componentResult = new List<ComponentResult>();
        }

        public string Content { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public decimal Total { get; set; }
        public string To { get; set; }
        public string EmailAdress { get; set; }
        public string NameSurname { get; set; }
        public string CompanyName { get; set; }
        public string Message { get; set; }
        public List<ComponentResult> componentResult { get; set; }
    }
}
