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
            var brevoApiKey = _configuration["Brevo:api-key"];
            var brevoApiUrl = _configuration["Brevo:api-url"]; // Adjust based on Brevo's API

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", brevoApiKey);

                var form = new Dictionary<string, string>
                {
                    { "from", "Jaswill Real Estate <aderibigbeolamide56@gmail.com>" },
                    { "to", $"{email.ReceiverName} <{email.ReceiverEmail}>" },
                    { "subject", email.Subject },
                    { "html", $"<html><body><h6>{email.Message}</h6></body></html>" }
                    // Add other Brevo-specific parameters as needed
                };

                var response = await client.PostAsync(brevoApiUrl, new FormUrlEncodedContent(form));

                return response.IsSuccessStatusCode;
            }
        }
    }
}
