using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Email
{
    public interface IEmailService
    {
        void SendEmail(string subject, string body, string to, string cc, string bcc);
    }
}
