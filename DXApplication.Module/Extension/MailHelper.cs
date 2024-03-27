using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using DevExpress.ExpressApp;
using System.Text;
using System.Collections.Generic;
using DXApplication.Module.BusinessObjects;

namespace DXApplication.Module.Extension
{
    public class MailHelper
    {
        public class EmailParameter
        {
            public string Host { get; set; }
            public int Port { get; set; }
            public string Account { get; set; }
            public string Password { get; set; }
            public string From { get; set; }
        }
        public static EmailParameter GetEmailParams(IObjectSpace objectSpace)
        {
            return new()
            {
                Host = objectSpace.GetObjectsQuery<ConfigurationMail>().FirstOrDefault(c => c.Key == "EmailHost").Value,
                Port = int.Parse(objectSpace.GetObjectsQuery<ConfigurationMail>().FirstOrDefault(c => c.Key == "EmailPort").Value),
                Account = objectSpace.GetObjectsQuery<ConfigurationMail>().FirstOrDefault(c => c.Key == "EmailAccount").Value,
                Password = objectSpace.GetObjectsQuery<ConfigurationMail>().FirstOrDefault(c => c.Key == "EmailPassword").Value,
                From = objectSpace.GetObjectsQuery<ConfigurationMail>().FirstOrDefault(c => c.Key == "EmailFrom").Value
            };
        }
        public static (string Header, string Body) GetEmailTemplate(string name, IObjectSpace objectSpace)
        {
            var template = objectSpace.GetObjectsQuery<EmailTemplate>().FirstOrDefault(c => c.Name == name);
            return (template.Header, template.Body);
        }
        public static void SendEmail(string from, string to, string subject, string body, EmailParameter parameter)
        {
            try
            {
                MailMessage message = new();
                SmtpClient smtp = new();
                message.From = new MailAddress(from);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = parameter.Port;
                smtp.Host = parameter.Host; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(parameter.Account, parameter.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                throw new UserFriendlyException("Có lỗi trong quá trình gửi email. Hãy liên hệ với quản trị viên để xử lý.");
            }
}

        public static string CreateBody(Dictionary<string, string> pairs, string template)
        {
            StringBuilder sb = new(template);
            foreach (var pair in pairs)
            {
                sb.Replace(pair.Key, pair.Value);
            }
            return sb.ToString();
        }
    }
}
