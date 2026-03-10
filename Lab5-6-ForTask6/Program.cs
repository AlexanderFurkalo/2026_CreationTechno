using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab5_6_ForTask6
{
    /// Subject
    public interface IWebServer
    {
        string GetPage(string url);
    }

    /// RealSubject
    public class RealWebServer : IWebServer
    {
        public string GetPage(string url)
        {
            Console.WriteLine("Fetching page from real web server...");
            Thread.Sleep(1500); 

            return $"<html>Content of {url}</html>";
        }
    }

    /// Proxy
    public class ProxyWebServer : IWebServer
    {
        private RealWebServer realServer;
        private Dictionary<string, string> cache;

        public ProxyWebServer()
        {
            cache = new Dictionary<string, string>();
        }

        public string GetPage(string url)
        {
            if (cache.ContainsKey(url))
            {
                Console.WriteLine("Returning cached page...");
                return cache[url];
            }

            Console.WriteLine("Page not in cache. Fetching from server...");

            if (realServer == null)
                realServer = new RealWebServer();

            string page = realServer.GetPage(url);

            cache[url] = page;

            return page;
        }
    }

    /// Client
    class Program
    {
        static void Main(string[] args)
        {
            IWebServer server = new ProxyWebServer();

            Console.WriteLine(server.GetPage("example.com"));
            Console.WriteLine();

            Console.WriteLine(server.GetPage("example.com"));
            Console.WriteLine();

            Console.WriteLine(server.GetPage("secondpage.com"));

            Console.ReadKey();
        }
    }
}
