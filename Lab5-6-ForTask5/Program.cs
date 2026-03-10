using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_6_ForTask5
{
    public interface IMessageSender
    {
        void Send(string message, string recipient);
    }

    public class EmailSender : IMessageSender
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"[EMAIL] Sending message to {recipient}: {message}");
        }
    }

    public class SMSsender
    {
        public void SendSMS(string phoneNumber, string text)
        {
            Console.WriteLine($"[SMS] Sending SMS to {phoneNumber}: {text}");
        }
    }

    public class SMSAdapter : IMessageSender
    {
        private SMSsender smsSender;

        public SMSAdapter()
        {
            smsSender = new SMSsender();
        }

        public void Send(string message, string recipient)
        {
            smsSender.SendSMS(recipient, message);
        }
    }

    public class TelegramSender
    {
        public void SendTelegramMessage(string username, string message)
        {
            Console.WriteLine($"[Telegram] Sending Telegram message to {username}: {message}");
        }
    }

    public class TelegramAdapter : IMessageSender
    {
        private TelegramSender telegramSender;

        public TelegramAdapter()
        {
            telegramSender = new TelegramSender();
        }

        public void Send(string message, string recipient)
        {
            telegramSender.SendTelegramMessage(recipient, message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            IMessageSender sender;

            sender = new EmailSender();
            sender.Send("Привіт! Відправляю тобі лист!", "user@email.com");

            sender = new SMSAdapter();
            sender.Send("Вітаю через смс!", "+380123456789");

            sender = new TelegramAdapter();
            sender.Send("Надсилаю повідомлення через Telegram!", "@username");

            Console.ReadKey();
        }
    }
}
