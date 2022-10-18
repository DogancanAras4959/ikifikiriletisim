using ikifikir.CORE.EmailConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.ENGINES.Interface
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(Message message);
        Task<string> SendEmailAsync(AppoinmentContact contact);

        //Task<string> SendEmailAsyncCalculate(AppoinmentContact contact);
    }
}
