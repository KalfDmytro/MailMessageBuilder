using System.Collections.Generic;

namespace MailMessageBuilderLib
{
    public interface IEmailToBuilder
    {
        /// <summary>
        /// Sets the To addresses for MailMessage
        /// </summary>
        /// <param name="emails">Email To adresses</param>
        /// <returns></returns>
        IEmailAdditionalBuilder To(IEnumerable<string> emails);
        /// <summary>
        /// Sets the To Adress/Adresses for MailMessage
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        IEmailAdditionalBuilder To(params string[] emails);
    }
}