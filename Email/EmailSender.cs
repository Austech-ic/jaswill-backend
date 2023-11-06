using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CMS_appBackend.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(EmailRequestModel email)
        {
            var mailgunApiKey = _configuration["Mailgun:api-key"];
            var mailgunDomain = _configuration["Mailgun:domain"];
            var mailgunApiUrl = $"https://api.mailgun.net/v3/{mailgunDomain}/messages";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{mailgunApiKey}")));

                var form = new Dictionary<string, string>
                {
                    { "from", "Jaswill Real Estate <aderibigbeolamide56@gmail.com>" },
                    { "to", $"{email.ReceiverName} <{email.ReceiverEmail}>" },
                    { "subject", email.Subject },
                    { "html", $"<html><body><h6>{email.Message}</h6></body></html>" }
                    // Add other Mailgun parameters as needed
                };

                var response = await client.PostAsync(mailgunApiUrl, new FormUrlEncodedContent(form));

                return response.IsSuccessStatusCode;
            }
        }
    }
}