namespace CMS_appBackend.Email
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(EmailRequestModel email, string resetLink);
    }
}
