using System;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! \n");
            Console.WriteLine("Writing to the cloud stand by... \n");
            Tables.runDemoAsync().Wait();
            Console.WriteLine("Ending Program \n");
        }
    }
}
