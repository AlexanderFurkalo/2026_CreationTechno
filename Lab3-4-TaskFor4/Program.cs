using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3_4_TaskFor4
{
    public sealed class Service
    {
        private static readonly Lazy<Service> instance =
            new Lazy<Service>(() => new Service());

        public static Service Instance => instance.Value;

        private bool isWorking = false;

        private readonly object lockObj = new object();

        private Service() { }

        public void DoWork(string message)
        {
            lock (lockObj)
            {
                isWorking = true;
            }

            Console.WriteLine("Service started...");
            Console.WriteLine(message);

            // Імітація роботи
            Thread.Sleep(2000);

            Console.WriteLine("Service finished.");

            lock (lockObj)
            {
                isWorking = false;
            }
        }

        public string GetStatus()
        {
            lock (lockObj)
            {
                return isWorking ? "Working" : "Idle";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Service.Instance.GetStatus());

            Thread thread = new Thread(() =>
            {
                Service.Instance.DoWork("Processing data...");
            });

            thread.Start();

            Thread.Sleep(500);
            Console.WriteLine(Service.Instance.GetStatus());

            thread.Join();

            Console.WriteLine(Service.Instance.GetStatus());

            Console.ReadKey();
        }
    }
}
