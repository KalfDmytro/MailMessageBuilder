using System.Net.Mail;

namespace MailMessageBuilderLib
{
    public interface IEmailFinalBuilder
    {
        /// <summary>
        /// Build filled MailMessage Object. Final method
        /// </summary>
        /// <returns></returns>
        MailMessage Build();
    }
}
