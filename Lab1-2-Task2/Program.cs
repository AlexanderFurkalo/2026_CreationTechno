using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2_Task2
{
    class Program
    {
        //Product
        class MailMessage
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            public void Show()
            {
                Console.WriteLine("\n----- ЛИСТ -----");
                Console.WriteLine($"Від: {From}");
                Console.WriteLine($"До: {To}");
                Console.WriteLine($"Заголовок: {Subject}");
                Console.WriteLine("Вміст повідомлення:");
                Console.WriteLine(Body);
                Console.WriteLine("-----------------\n");
            }
        }

        //Director
        class Director
        {
            public void Construct(MailBuilder builder)
            {
                builder
                    .CreateNewMessage()
                    .BuildFrom()
                    .BuildTo()
                    .BuildSubject()
                    .BuildBody();
            }
        }

        //Builder
        abstract class MailBuilder
        {
            protected MailMessage message;

            public MailBuilder CreateNewMessage()
            {
                message = new MailMessage();
                return this;
            }

            public abstract MailBuilder BuildFrom();
            public abstract MailBuilder BuildTo();
            public abstract MailBuilder BuildSubject();
            public abstract MailBuilder BuildBody();

            public MailMessage GetResult()
            {
                return message;
            }
        }

        //ConcreteBuilder1
        class BusinessMailBuilder : MailBuilder
        {
            public override MailBuilder BuildFrom()
            {
                message.From = "company@business.com";
                return this;
            }

            public override MailBuilder BuildTo()
            {
                message.To = "client@domain.com";
                return this;
            }

            public override MailBuilder BuildSubject()
            {
                message.Subject = "Business Proposal";
                return this;
            }

            public override MailBuilder BuildBody()
            {
                message.Body = "Let us negotiate our cooperation.";
                return this;
            }
        }

        //ConcreteBuilder2
        class PersonalMailBuilder : MailBuilder
        {
            public override MailBuilder BuildFrom()
            {
                message.From = "myhomemail@email.com";
                return this;
            }

            public override MailBuilder BuildTo()
            {
                message.To = "bestfriendo@email.com";
                return this;
            }

            public override MailBuilder BuildSubject()
            {
                message.Subject = "Зустрінемось?";
                return this;
            }

            public override MailBuilder BuildBody()
            {
                message.Body = "Привіт! Давно не бачилися. Зустрінемося завтра в парку?";
                return this;
            }
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Director director = new Director();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Mail Template Menu");
                Console.WriteLine("Виберіть команду:");
                Console.WriteLine("1 - Ділове повідомлення");
                Console.WriteLine("2 - Персональне повідомлення");
                Console.WriteLine("0 - Вихід\n");

                string choice = Console.ReadLine();
                MailBuilder builder = null;

                switch (choice)
                {
                    case "0":
                        exit = true;
                        continue;

                    case "1":
                        builder = new BusinessMailBuilder();
                        break;

                    case "2":
                        builder = new PersonalMailBuilder();
                        break;

                    default:
                        Console.WriteLine("Натиснута кнопка не викликає жодних команд.Будь ласка, виберіть іншу.");
                        continue;
                }

                director.Construct(builder);
                MailMessage mail = builder.GetResult();
                mail.Show();
            }

            Console.WriteLine("Програму було завершено.");
            Console.ReadKey();
        }
    }
}

