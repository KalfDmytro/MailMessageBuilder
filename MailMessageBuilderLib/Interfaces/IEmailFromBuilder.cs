namespace MailMessageBuilderLib
{
    public interface IEmailFromBuilder
    {
        /// <summary>
        /// Sets the From address for MailMessage. 
        /// </summary>
        /// <param name="emailFrom">Email From address</param>
        /// <returns></returns>
        IEmailToBuilder From(string emailFrom);
    }
}