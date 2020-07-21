using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace MailMessageBuilderLib
{
    public interface IEmailAdditionalBuilder : IEmailToBuilder, IEmailFinalBuilder
    {
        /// <summary>
        /// Set data object and replace placeholders in Body and Subject by values of data object's properties.
        /// <para>Body: 'Good day, dear [FirstName]!'</para>
        /// <para>DataObject Property: dataObject.FirstName = "Paul"</para>
        /// <para>Result: 'Good day, dear Paul!'</para>
        /// </summary>
        /// <param name="dataObject">
        /// Data object to replace placehodlers in template: [PARAM1] -> DataObject.PARAM1. Name of properties should be exactly the same like placeholders in Html Template but without brackets []. You can see sample of data object in documentation for this method.
        /// </param>
        /// 
        /// <returns></returns>
        /// <example>
        /// <code>
        /// public class ReplacementObjectClass
        /// {
        ///     public string PARAM1 { get; set }
        /// }
        /// 
        /// Html template shoud has [PARAM1] text value in the body
        /// </code>
        /// </example>
        IEmailAdditionalBuilder SetTextReplacmentObject(object dataObject);
        /// <summary>
        /// Set data object and replace placeholders in Body and Subject by values of data object's properties.
        /// <para>Body: 'Good day, dear [FirstName]!'</para>
        /// <para>DataObject Property: dataObject.FirstName = "Paul"</para>
        /// <para>Result: 'Good day, dear Paul!'</para>
        /// </summary>
        /// <param name="dataObject">
        /// Data object to replace placehodlers in template: [PARAM1] -> DataObject.PARAM1. Name of properties should be exactly the same like placeholders in Html Template but without brackets []. You can see sample of data object in documentation for this method.
        /// </param>
        /// <param name="leftSign">Left Sign in template, example: [PARAM1] - left sign is "["</param>
        /// <param name="rightSign">Right Sign in template, example: [PARAM1] - right sign is "]"</param>
        /// 
        /// <returns></returns>
        /// <example>
        /// <code>
        /// public class ReplacementObjectClass
        /// {
        ///     public string PARAM1 { get; set }
        /// }
        /// 
        /// Html template shoud has [PARAM1] text value in the body
        /// </code>
        /// </example>
        IEmailAdditionalBuilder SetTextReplacmentObject(object dataObject, string leftSign, string rightSign);
        /// <summary>
        /// Add Attachment to the MailMessage by file path and set ContentId to it to correctly display in Html Template.
        /// <para><paramref name="contentId"/> should be equal to 'cid:*' value in Html Template</para>
        /// <para>Example: If you have img in html template, then Source should be: src='cid:specific_content_id', and <paramref name="contentId"/> = specific_content_id</para>
        /// </summary>
        /// <param name="attachmentPath">Physical path to the attachment file</param>
        /// <param name="contentId">ContentId string that should be equal to 'cid' in the HtmlTemplate</param>
        /// <returns></returns>
        IEmailAdditionalBuilder AddAttachment(string attachmentPath, string contentId);
        /// <summary>
        /// Add Attachment to the MailMessage with Stream and set ContentId to it to correctly display in Html Template.
        /// <para><paramref name="contentId"/> should be equal to 'cid:*' value in Html Template</para>
        /// <para>Example: If you have img in html template, then Source should be: src='cid:specific_content_id', and <paramref name="contentId"/> = specific_content_id</para>
        /// </summary>
        /// <param name="contentStream"></param>
        /// <param name="name"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        IEmailAdditionalBuilder AddAttachment(Stream contentStream, string name, string contentId);
        /// <summary>
        /// Prevent sending emails on DEV environment to the real receivers, and don't send it to CC and BCC, but send it to <paramref name="emailToOnDev"/> instead.
        /// </summary>
        /// <param name="emailToOnDev">Dev Email Receiver</param>
        /// <returns></returns>
        IEmailFinalBuilder ToOnDevEnvironment(bool isDevEnvironment, params string[] emailToOnDev);
        /// <summary>
        /// Prevent sending emails on DEV environment to the real receivers, and don't send it to CC and BCC, but send it to <paramref name="emailToOnDev"/> instead.
        /// </summary>
        /// <param name="emailsToOnDev">Dev Email Receivers</param>
        /// <returns></returns>
        IEmailFinalBuilder ToOnDevEnvironment(bool isDevEnvironment, IEnumerable<string> emailsToOnDev);
        /// <summary>
        /// Add blind carbon copy (BCC) recipient/recepients to MailMessage
        /// </summary>
        /// <param name="email">BCC Recepient email address</param>
        /// <returns></returns>
        IEmailAdditionalBuilder Bcc(params string[] emails);
        /// <summary>
        /// Add blind carbon copy (BCC) recipients to MailMessage
        /// </summary>
        /// <param name="emails">BCC Recepients email addresses</param>
        /// <returns></returns>
        IEmailAdditionalBuilder Bcc(IEnumerable<string> emails);
        /// <summary>
        /// Add carbon copy (CC) recipient/recepients to MailMessage
        /// </summary>
        /// <param name="email">CC Recepient email address</param>
        /// <returns></returns>
        IEmailAdditionalBuilder CC(params string[] emails);
        /// <summary>
        /// Add carbon copy (CC) recipients to MailMessage
        /// </summary>
        /// <param name="emails">CC Recepients email addresses</param>
        /// <returns></returns>
        IEmailAdditionalBuilder CC(IEnumerable<string> emails);
    }
}