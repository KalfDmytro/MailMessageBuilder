using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace MailMessageBuilderLib
{
    /// <summary>
    /// Builder for <see cref="System.Net.Mail.MailMessage"/>
    /// </summary>
    public class MailMessageBuilder : IEmailSubjectBuilder, IEmailBodyBuilder, IEmailFromBuilder, IEmailToBuilder, IEmailAdditionalBuilder, IEmailFinalBuilder
    {
        private MailMessage Message { get; }
        private StringBuilder bodyText;
        private StringBuilder subjectText;

        /// <summary>
        /// Private constructor to prevent creating object and force user to use CreateEmailMessage method
        /// </summary>
        private MailMessageBuilder()
        {
            Message = new MailMessage();
            Message.BodyEncoding = Encoding.UTF8;
            Message.SubjectEncoding = Encoding.UTF8;
            Message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        }

        /// <summary>
        /// Start creation of the MailMessage object
        /// </summary>
        /// <param name="isHtmlBody">Is body in Html format?</param>
        /// <returns></returns>
        public static IEmailSubjectBuilder CreateEmailMessage(bool isHtmlBody)
        {
            MailMessageBuilder builder = new MailMessageBuilder();
            builder.Message.IsBodyHtml = isHtmlBody;
            return builder;
        }

        public IEmailBodyBuilder Subject(string subject)
        {
            subjectText = new StringBuilder(subject);
            return this;
        }

        IEmailFromBuilder IEmailBodyBuilder.Body(string body)
        {
            bodyText = new StringBuilder(body);
            return this;
        }

        public IEmailToBuilder From(string emailFrom)
        {
            Message.From = new MailAddress(emailFrom);
            return this;
        }

        public IEmailAdditionalBuilder To(IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                Message.To.Add(email);
            }

            return this;
        }

        public IEmailAdditionalBuilder To(params string[] emails)
        {
            return To(emails.AsEnumerable());
        }

        public IEmailAdditionalBuilder SetTextReplacmentObject(object dataObject)
        {
            return SetTextReplacmentObject(dataObject, "[", "]");
        }

        public IEmailAdditionalBuilder SetTextReplacmentObject(object dataObject, string leftSign, string rightSign)
        {
            Type objectType = dataObject.GetType();
            var publicProperties = objectType.GetProperties();

            foreach (var prop in publicProperties)
            {
                subjectText.Replace($"{leftSign}{prop.Name}{rightSign}", prop.GetValue(dataObject).ToString());
                bodyText.Replace($"{leftSign}{prop.Name}{rightSign}", prop.GetValue(dataObject).ToString());
            }
            return this;
        }

        public IEmailAdditionalBuilder AddAttachment(string attachmentPath, string contentId)
        {
            Attachment attachment = new Attachment(attachmentPath);
            attachment.ContentId = contentId;
            Message.Attachments.Add(attachment);
            return this;
        }

        public IEmailAdditionalBuilder AddAttachment(Stream contentStream, string name, string contentId)
        {
            Attachment attachment = new Attachment(contentStream, name);
            attachment.ContentId = contentId;
            Message.Attachments.Add(attachment);
            return this;
        }

        public MailMessage Build()
        {
            Message.Subject = subjectText.ToString();
            Message.Body = bodyText.ToString();
            return Message;
        }

        public IEmailAdditionalBuilder Bcc(params string[] emails)
        {
            return Bcc(emails.AsEnumerable());
        }

        public IEmailAdditionalBuilder Bcc(IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                Message.Bcc.Add(email);
            }
            return this;
        }

        public IEmailAdditionalBuilder CC(params string[] emails)
        {
            return CC(emails.AsEnumerable());
        }

        public IEmailAdditionalBuilder CC(IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                Message.CC.Add(email);
            }
            return this;
        }

        public IEmailFinalBuilder ToOnDevEnvironment(bool isDevEnvironment, params string[] emailsToOnDev)
        {
            return ToOnDevEnvironment(isDevEnvironment, emailsToOnDev.AsEnumerable());
        }

        public IEmailFinalBuilder ToOnDevEnvironment(bool isDevEnvironment, IEnumerable<string> emailsToOnDev)
        {
            if (isDevEnvironment)
            {
                Message.Bcc.Clear();
                Message.CC.Clear();
                Message.To.Clear();
                foreach (var email in emailsToOnDev)
                    Message.To.Add(email);
            }
            return this;
        }
    }
}
