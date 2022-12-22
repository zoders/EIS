using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace Practice2
{
    abstract class Builder
    {
        public abstract void BuildAddress(string fromAddr, string toAddr);
        public abstract void BuildTheme();
        public abstract void BuildText(string message);
        public abstract void BuildAttachment();
        public abstract MailMessage GetResult();
    }
    class MessageBuilder : Builder
    {
        MailMessage mail;

        public override void BuildAddress(string fromAddr, string toAddr)
        {
            MailAddress from = new MailAddress(fromAddr);
            MailAddress to = new MailAddress(toAddr);
            mail = new MailMessage(from, to);
        }

        public override void BuildAttachment()
        {
            mail.Attachments.Add(new Attachment("message.txt"));
        }

        public override void BuildText(string message)
        {
            mail.IsBodyHtml = false;
            mail.Body = message;
        }

        public override void BuildTheme()
        {
            mail.Subject = "Hello guys";
        }

        public override MailMessage GetResult()
        {
            return mail;
        }
    }
    class MessageSender
    {
        Builder build;

        public MessageSender(Builder builder)
        {
            this.build = builder;
        }

        public void CreateMessage(string fromAddr, string toAddr, string message)
        {
            build.BuildAddress(fromAddr, toAddr);
            build.BuildTheme();
            build.BuildText(message);
            build.BuildAttachment();
        }

        public void SendMessage(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587);
            smtp.Credentials = new NetworkCredential(mail.From.Address, "your_pswd");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
    class Facade {
        MessageBuilder builder;
        MessageSender sender;
        public Facade(MessageBuilder _builder, MessageSender _sender)
        {
            builder = _builder;
            sender = _sender;
        }
        public void SendMessageToUserWithFollowingText(string user, string text)
        {
            sender.CreateMessage("redcherries_af@hotmail.com", user, text);
            sender.SendMessage(builder.GetResult());
            Console.WriteLine("Message is delivered");
        }
    }//

    class Program
    {
        static void Main(string[] args)
        {
            MessageBuilder builder = new MessageBuilder();
            Facade facade = new Facade(builder, new MessageSender(builder));
            facade.SendMessageToUserWithFollowingText("sanyafil61802@gmail.com", "Get this file hehehe");
            
            
        }
    }
}
