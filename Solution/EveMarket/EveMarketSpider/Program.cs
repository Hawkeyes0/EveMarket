using System;

namespace EveMarketSpider
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            EveSpider spider = new EveSpider();
            await spider.CatchDataAsync();
        }
    }
}
