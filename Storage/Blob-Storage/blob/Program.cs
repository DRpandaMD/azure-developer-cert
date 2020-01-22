using System;

namespace blob
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Blobs.RunAsync().Wait();
        }
    }
}
