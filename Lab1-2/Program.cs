using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_2
{
    abstract class FileDocument
    {
        public abstract void Create(string name);
    }

    class TextFile : FileDocument
    {
        public override void Create(string name)
        {
            Console.WriteLine($"Текстовий файл '{name}.txt' створено.");
        }
    }

    class ImageFile : FileDocument
    {
        public override void Create(string name)
        {
            Console.WriteLine($"Файл зображення '{name}.png' створено.");
        }
    }

    abstract class FileCreator
    {
        public abstract FileDocument FactoryMethod();
    }

    class TextFileCreator : FileCreator
    {
        public override FileDocument FactoryMethod()
        {
            return new TextFile();
        }
    }

    class ImageFileCreator : FileCreator
    {
        public override FileDocument FactoryMethod()
        {
            return new ImageFile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Виберіть тип файлу");
                Console.WriteLine("1 - Текстовий файл");
                Console.WriteLine("2 - Графічний файл");
                Console.WriteLine("0 - Вихід\n");

                string choice = Console.ReadLine();

                FileCreator creator = null;

                switch (choice)
                {
                    case "0":
                        exit = true;
                        continue;

                    case "1":
                        creator = new TextFileCreator();
                        break;

                    case "2":
                        creator = new ImageFileCreator();
                        break;

                    default:
                        Console.WriteLine("Натиснута кнопка не викликає жодних команд. Будь ласка, виберіть іншу.");
                        Console.ReadKey();
                        continue;
                }

                FileDocument file = creator.FactoryMethod();

                Console.Write("Введіть назву файлу: ");
                string name = Console.ReadLine();

                file.Create(name);

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися до меню ");
                Console.ReadLine();
            }
            Console.WriteLine("Програму було завершено.");
        }
    }
}
