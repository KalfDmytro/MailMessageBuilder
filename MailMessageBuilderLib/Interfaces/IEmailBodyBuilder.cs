namespace MailMessageBuilderLib
{
    public interface IEmailBodyBuilder
    {
        /// <summary>
        /// Sets body to MailMessage
        /// </summary>
        /// <param name="body">Body text</param>
        /// <returns></returns>
        IEmailFromBuilder Body(string body);
    }
}