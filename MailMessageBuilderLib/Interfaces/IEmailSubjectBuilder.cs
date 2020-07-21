namespace MailMessageBuilderLib
{
    public interface IEmailSubjectBuilder
    {
        /// <summary>
        /// Sets Subject for MailMessage
        /// </summary>
        /// <param name="subject">Subject text</param>
        /// <returns></returns>
        IEmailBodyBuilder Subject(string subject);
    }
}