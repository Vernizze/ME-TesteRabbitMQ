using System;

namespace TesteRabbitMQ.MassTransit.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.ConfigureServices();

            Console.WriteLine("Hello World!");
        }
    }
}
