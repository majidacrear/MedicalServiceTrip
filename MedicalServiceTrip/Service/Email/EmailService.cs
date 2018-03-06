using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Service.Email
{
    public class EmailService : IEmailService
    {
        #region Fields

        private readonly IWebHelper _webHelper;

        #endregion

        #region Cros

        public EmailService(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        
        public async void SendEmail(string subject, string body, string to, string cc, string bcc)
        {
            var client = new SendGridClient(_webHelper.GetSendGridApiKey());
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_webHelper.GetFromEmail()),
                Subject = subject,
                HtmlContent = body
            };
            msg.AddTo(new EmailAddress(to));
            var response = await client.SendEmailAsync(msg);
            
        }

        #endregion
    }
}
